#  [![Typing SVG](https://readme-typing-svg.herokuapp.com/?color=ffff&size=35&center=true&vCenter=true&width=1000&lines=Bem-vindo(a)!+:%29;Projeto+Back-End;IQuirium;Sistema+de+Feedback)](https://git.io/typing-svg)

## Resumo
A plataforma IQuirium oferece duas funcionalidades de feedback. A "Captura de Feedbacks sobre o Produto" permite aos usuários fornecer opiniões sobre uso, funcionalidades e melhorias através de formulários ou questionários rápidos, facilitando a coleta de insights. A funcionalidade "Envio e Recebimento de Feedbacks entre Usuários" promove colaboração e desenvolvimento pessoal, permitindo que os usuários enviem ou solicitem feedbacks sobre competências, comportamentos e atividades. Essa troca ajuda no crescimento contínuo e autodesenvolvimento entre colegas e mentores.

### 1. Feedbacks do Produto
Permite que os usuários forneçam feedbacks sobre o uso do produto, incluindo sugestões de melhoria, relato de problemas técnicos e comentários gerais.

#### Fluxo de Usuário
1. **Envio de Feedback Espontâneo**: O usuário acessa a seção de feedbacks no menu da plataforma, preenche um formulário com as informações desejadas e envia o feedback.
2. **Confirmação de Envio**: Após o envio, o usuário recebe uma confirmação e uma mensagem de agradecimento.

#### Requisitos Técnicos
- **Banco de Dados**: Armazenamento de feedbacks com:
  - ID do usuário.
  - Texto do feedback.
  - Tipo de feedback (sugestão, comentário, erro).
  - Data e hora do envio.
- **Backend**: APIs para receber e armazenar feedbacks com autenticação e autorização de usuários autenticados.

#### Regras de Negócio
1. Somente usuários autenticados podem enviar feedbacks.
2.  Não há limites para o número de feedbacks enviados por usuário.

#### Métricas de Sucesso
- Taxa de envio de feedbacks.
- Número total de feedbacks.
- Classificação dos feedbacks por categoria.
- Engajamento de usuários recorrentes.

### Diagrama de Classes
![feedbackProduto (1)](https://github.com/user-attachments/assets/e35042f3-d6b7-4bb8-bda1-4b89217f17e7)



### Diagrama de Casos de Uso
![feedback-produto1](https://github.com/user-attachments/assets/eac3195b-ed3a-46f0-a020-2eb15a114a3f)


### Diagrama de Sequência
![Feedback-Porduto](https://github.com/user-attachments/assets/143c5bd6-60ce-450e-9a68-81ac60560d0c)

### 2. Feedbacks entre Usuários
Facilita a troca de feedbacks entre os usuários, com o objetivo de fomentar a colaboração e o desenvolvimento pessoal.

#### Fluxo de Usuário
1. **Solicitar Feedback**: O usuário seleciona o destinatário e especifica o tipo de feedback desejado.
2. **Enviar Feedback Espontâneo**: Permite o envio de feedback direto para outro usuário.
3. **Visualizar Feedback Recebido**: O usuário pode visualizar e responder ao feedback recebido.
4. **Reportar Feedback**: Em casos de conteúdo ofensivo ou impróprio, o usuário pode reportar o feedback para revisão pela equipe.

#### Requisitos Técnicos
- **Banco de Dados**: Armazenamento de feedbacks entre usuários e logs de reports, incluindo:
  - ID do remetente e destinatário.
  - Tipo de feedback.
  - Data e hora do envio.
- **Backend**: APIs para envio, recebimento e report de feedbacks, além de um sistema de moderação para revisão de feedbacks reportados.

#### Regras de Negócio
1. Feedbacks são privados, visíveis apenas para o remetente e destinatário.
2. Feedbacks podem ser enviados espontaneamente ou mediante solicitação.
3. Os usuários podem responder a feedbacks e reportar feedbacks impróprios.

#### Métricas de Sucesso
- Número de feedbacks enviados.
- Taxa de resposta a solicitações de feedback.
- Taxa de feedbacks reportados.
- Engajamento de usuários recorrentes.

### Diagrama de Classes
![feedback-usuario2](https://github.com/user-attachments/assets/02523f41-6615-4096-8cd0-729a8a3bca5b)

### Diagrama de Casos de uso
![feedback-Usuario](https://github.com/user-attachments/assets/f7b50e99-c796-4ca7-8038-1c230390d926)


### Diagrama de Sequência
![Feedback-usuario1](https://github.com/user-attachments/assets/320b8c5f-90f6-4e74-bf34-0cb41462868f)
