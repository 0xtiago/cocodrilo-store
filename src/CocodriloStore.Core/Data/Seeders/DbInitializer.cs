using CocodriloStore.Core.Data.Context;
using CocodriloStore.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CocodriloStore.Core.Data.Seeders;

public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            // Aplica todas as migrações pendentes
            context.Database.Migrate();
            
            // Verifica se já existem dados
            if (context.Categorias.Any() || context.Produtos.Any())
            {
                return; // Banco já está populado
            }
            
            // Criar papéis, se necessário
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            
            if (!await roleManager.RoleExistsAsync("Vendedor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Vendedor"));
            }

            // Criar usuário administrador de exemplo
            var adminUser = new IdentityUser
            {
                UserName = "admin@cocodrilo.com.br",
                Email = "admin@cocodrilo.com.br",
                EmailConfirmed = true
            };
            
            if (await userManager.FindByEmailAsync(adminUser.Email) == null)
            {
                await userManager.CreateAsync(adminUser, "Admin@123456");
                await userManager.AddToRoleAsync(adminUser, "Admin");
                
                // Criar vendedor associado ao admin
                var adminSeller = new Vendedor
                {
                    Nome = "Administrador",
                    Email = adminUser.Email,
                    UsuarioId = adminUser.Id
                };
                
                context.Vendedores.Add(adminSeller);
            }
            
            // Criar categorias iniciais
            var categories = new List<Categoria>
            {
                new Categoria { Nome = "Eletrônicos", Descricao = "Dispositivos e gadgets eletrônicos" },
                new Categoria { Nome = "Roupas", Descricao = "Camisetas, calças, vestidos e mais" },
                new Categoria { Nome = "Livros", Descricao = "Livros físicos e e-books" },
                new Categoria { Nome = "Casa e Decoração", Descricao = "Itens para sua casa" }
            };
            
            context.Categorias.AddRange(categories);
            await context.SaveChangesAsync();
        }
    }
