# 💰 Cashly

O **Cashly** é uma aplicação de controle financeiro pessoal, pensada para ajudar usuários a organizarem suas finanças de forma simples, visual e consciente.  
A proposta é funcionar como um **caderno de anotações digital**, que pode ser utilizado de forma individual ou compartilhado com a família.

O projeto é desenvolvido com base em **Domain-Driven Design (DDD)** e **Clean Architecture**, servindo tanto como um produto funcional quanto como um **projeto de portfólio**, com foco em boas práticas de arquitetura de software e evolução contínua.

---

## 🎯 Objetivo do Projeto

- Organizar entradas e saídas financeiras de forma clara e objetiva
- Permitir a visualização da saúde financeira mês a mês
- Oferecer um controle manual e consciente, sem integrações complexas com instituições financeiras
- Explorar e aplicar conceitos sólidos de arquitetura de software
- Servir como base de estudo, aprendizado e evolução técnica

---

## 🧠 Conceitos-Chave

- Cada **transação** é representada como um *card* (inspirado em Kanban)
- Os *cards* são organizados visualmente por **mês**
- Cada mês apresenta:
  - saldo acumulado
  - saldo projetado (considerando transações agendadas)
  - status financeiro (indicadores de saúde do mês)
- O domínio é responsável por garantir todas as regras de negócio
- O frontend consome apenas **casos de uso**, sem acessar regras internas do domínio

---

## 🏗️ Arquitetura

O Cashly segue os princípios da **Clean Architecture**, com separação clara de responsabilidades e foco em um **domínio rico**, expressivo e protegido.

### Camadas principais:

- **Domain**  
  Contém as entidades, value objects, enums e regras de negócio.  
  É a camada central do sistema e não depende de nenhuma outra.

- **Application**  
  Orquestra os casos de uso da aplicação, aplicando regras de fluxo, validações de entrada e tratamento de exceções de domínio.

- **Infrastructure**  
  Responsável por persistência de dados, integrações externas e implementações técnicas.

- **API**  
  Exposição dos casos de uso por meio de endpoints REST.

- **Frontend**  
  Interface construída em Angular, focada em experiência do usuário e clareza visual.

---

## ⚙️ Tecnologias Utilizadas

### Backend
- .NET (C#)
- ASP.NET Web API
- Entity Framework Core
- PostgreSQL
- Docker

### Frontend
- Angular
- TypeScript
- RxJS
- HTML e CSS

---

## 🧩 Decisões Técnicas

- Utilização de **Value Objects** para conceitos do domínio (ex.: `Money`, `Period`)
- Criação de **DomainException** para violações de regras de negócio
- Validações críticas concentradas no domínio
- Validações de entrada e UX na camada Application/UI
- Queries explícitas (sem Lazy Loading)
- Entities e Value Objects preferencialmente `sealed`

---

## 🚧 Status do Projeto

🔨 **Em desenvolvimento**

Atualmente o projeto está focado em:
- Estruturação da arquitetura
- Modelagem do domínio
- Definição das regras financeiras
- Criação dos primeiros casos de uso

---

## 🗺️ Roadmap (resumido)

- [ ] Estrutura inicial do backend
- [ ] Modelagem de entidades e value objects
- [ ] Casos de uso de transações
- [ ] Dashboard financeiro mensal
- [ ] Filtro global de exceções na camada Application
- [ ] Integração frontend-backend
- [ ] Testes automatizados de domínio e aplicação

---

## 🚀 Execução do Projeto

As instruções de instalação e execução serão adicionadas conforme o projeto evoluir.

---

## 👨‍💻 Autor

Projeto desenvolvido por **Gabriel Leal**, com foco em arquitetura de software, backend .NET e desenvolvimento fullstack.

---

## 📌 Observação

O Cashly é um projeto em constante evolução.  
Decisões arquiteturais e regras de negócio podem ser ajustadas conforme o aprendizado e o amadurecimento do domínio.