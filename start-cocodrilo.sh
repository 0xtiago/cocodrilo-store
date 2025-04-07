#!/bin/bash

MVC_PORT=5000
API_PORT=5001

# Função para matar processo que usa uma porta
kill_port_if_busy() {
  local PORT=$1
  local PID=$(lsof -ti tcp:$PORT)

  if [ -n "$PID" ]; then
    echo "⛔ Encerrando processo na porta $PORT (PID: $PID)..."
    kill -9 $PID
  fi
}

# Limpar processos antigos
kill_port_if_busy $MVC_PORT
kill_port_if_busy $API_PORT

# Iniciar projetos
echo "🚀 Iniciando CocodriloStore MVC na porta $MVC_PORT..."
dotnet run --project src/CocodriloStore.Web --urls="https://localhost:$MVC_PORT" > logs-mvc.log 2>&1 &

echo "🚀 Iniciando CocodriloStore API na porta $API_PORT..."
dotnet run --project src/CocodriloStore.Api --urls="https://localhost:$API_PORT" > logs-api.log 2>&1 &

sleep 1
echo "✅ Ambos os projetos foram iniciados!"
echo "🌐 MVC:  https://localhost:$MVC_PORT"
echo "🔧 API:  https://localhost:$API_PORT/swagger"
echo "📄 Logs salvos em: logs-mvc.log e logs-api.log"