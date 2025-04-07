#!/bin/bash

echo "⛔ Encerrando CocodriloStore MVC (porta 5000)..."
lsof -ti tcp:5000 | xargs kill -9

echo "⛔ Encerrando CocodriloStore API (porta 5001)..."
lsof -ti tcp:5001 | xargs kill -9

echo "✅ Aplicações finalizadas!"
