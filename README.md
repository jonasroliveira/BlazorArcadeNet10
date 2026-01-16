# 🕹️ Blazor Arcade Collection
### Este repositório contém dois jogos experimentais desenvolvidos com Blazor WebAssembly, explorando desde a lógica 2D clássica até motores gráficos 3D modernos.

## 🐍 1. Classic Snake Game
- Um tributo ao jogo da cobrinha clássico, focado em lógica de matrizes e manipulação de estado em C#.
- Tecnologia: Blazor (C#) & HTML5 Canvas / CSS Grid.

### Destaques:
- Lógica de crescimento da cobra ao comer.
- Detecção de colisão com paredes e com o próprio corpo.
- Aumento de velocidade conforme a pontuação sobe.
- Controles: Setas do teclado.

___

## 🔴 2. Survival 3D
- Um jogo de sobrevivência em primeira pessoa com estética retro, utilizando Three.js integrado ao ecossistema .NET.
- Tecnologia: Blazor WebAssembly + Three.js (JavaScript Interop).

### Mecânicas:
- Motor 3D: Renderização de luzes, neblina e objetos em tempo real.
- Raycasting: Sistema de colisão preciso entre raios laser e o muro.
- Interoperabilidade: O JavaScript detecta a colisão e avisa o C# para atualizar a interface.
- Objetivo: Proteja seu muro vermelho! Evite os raios rosas que surgem do fundo. Cada acerto retira 10% de energia.

## 🛠️ Arquitetura Técnica
- A grande vantagem deste projeto é a Comunicação Bidirecional:
- C# para JS: O Blazor envia comandos de movimento e inicializa a cena 3D.
- JS para C#: O motor Three.js utiliza DotNetObjectReference para invocar métodos C# e atualizar o placar/energia sem recarregar a página.
