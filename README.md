# gameStore

Um projeto em C# representando uma API de uma loja de jogos.

## Publicando

~~~ sh
docker build -t game-store:{versao} . # para gerar imagem do docker executável
docker run -p 8080:8080 game-store:{versao} # para criar o container da aplicação
~~~ sh

Obs: versão no formato 0.0.1