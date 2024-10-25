### Estrutura da Base de Dados para Feedbacks da Plataforma IQuirium

#### Introdução

A estrutura da base de dados da plataforma IQuirium foi desenvolvida pensando em atender às necessidades de rastreabilidade, organização e interação entre os usuários, garantindo feedbacks claros e seguros. A seguir, apresentamos as tabelas criadas e seus objetivos, explicando o raciocínio por trás de cada decisão.

#### Estrutura das Tabelas

##### 1. Tabela `users`
Pensamos em criar a tabela `users` para armazenar informações básicas dos usuários, garantindo que todos os feedbacks fossem vinculados corretamente e mantendo a rastreabilidade das interações na plataforma. Dessa forma, conseguimos organizar as atividades de maneira clara e estruturada. Os campos são:

- **id**: Identificador único do usuário.
- **username**: Nome do usuário na plataforma.
- **email**: Email do usuário para comunicação e autenticação.
- **role**: Papel do usuário (administrador, membro, etc.).
- **created_at**: Data de criação do usuário.

##### 2. Tabela `products`
Pensamos em criar a tabela `products` para armazenar informações sobre os produtos disponíveis para avaliação pelos usuários, facilitando o processo de feedback e ajudando outros usuários a tomar decisões mais informadas. Os campos são:

- **id**: Identificador único do produto.
- **title**: Título do produto.
- **description**: Descrição detalhada do produto.
- **price**: Preço do produto.
- **status**: Status do produto (ativo, inativo).
- **created_at**: Data de criação do produto.

##### 3. Tabela `product_feedbacks`
Pensamos em criar a tabela `product_feedbacks` para permitir que os usuários compartilhassem suas opiniões sobre os produtos, ajudando outros usuários e a equipe da plataforma a melhorar a qualidade oferecida. Os campos são:

- **id**: Identificador único do feedback.
- **product_id**: Referência ao produto avaliado.
- **user_id**: Referência ao usuário que fez o feedback.
- **rating**: Avaliação numérica do produto (1-5 estrelas).
- **comment**: Comentário sobre o produto.
- **created_at**: Data e hora do feedback.

##### 4. Tabela `user_feedbacks`
Pensamos em criar a tabela `user_feedbacks` para permitir feedbacks entre usuários, podendo ser abertos ou direcionados. Também era importante possibilitar conversas em threads, com respostas aos feedbacks. Assim, acreditamos que poderíamos criar um ambiente colaborativo onde os usuários pudessem trocar ideias e melhorar mutuamente. Os campos são:

- **id**: Identificador único do feedback.
- **parent_feedback_id**: ID do feedback anterior, para permitir respostas e criar uma conversa.
- **sender_id**: Referência ao remetente do feedback.
- **receiver_id**: Referência ao destinatário do feedback, ou `null` se for um feedback aberto.
- **feedback_type**: Tipo de feedback (competências, comportamentos, etc.).
- **content**: Conteúdo do feedback.
- **is_open**: Indica se o feedback está aberto para qualquer usuário responder.
- **created_at**: Data e hora do envio do feedback.

##### 5. Tabela `feedback_reports`
Pensamos em criar a tabela `feedback_reports` para garantir um ambiente seguro, permitindo que feedbacks impróprios fossem reportados e revisados. Dessa forma, conseguimos manter um espaço respeitoso e adequado para todos os usuários. Os campos são:

- **id**: Identificador único do report.
- **feedback_id**: Referência ao feedback reportado.
- **reported_by**: Referência ao usuário que fez o report.
- **reason**: Motivo do report (conteúdo ofensivo, impróprio, etc.).
- **status**: Status do report (pendente, revisado, ação tomada).
- **created_at**: Data e hora do report.

#### Conclusão

Pensamos em cada detalhe da estrutura da base de dados da plataforma IQuirium para que fosse simples e eficiente, atendendo às necessidades dos usuários para interação e feedback. Cada tabela foi desenvolvida com base nos requisitos apresentados, promovendo feedbacks construtivos, colaborativos e seguros, garantindo a melhor experiência para os usuários e possibilitando o crescimento da plataforma.
