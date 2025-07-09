# Cashly - App
![Angular](https://img.shields.io/badge/Angular-20.0.5-DD0031?logo=angular&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-Strict-3178C6?logo=typescript&logoColor=white)
![SCSS](https://img.shields.io/badge/SCSS-%23CD6799?logo=sass&logoColor=white)
![RxJS](https://img.shields.io/badge/RxJS-7+-B7178C?logo=reactivex&logoColor=white)

Frontend da aplicação **Cashly**, desenvolvida em Angular20 com arquitetura baseada em **Clean Architecture** e princípios de **DDD (Domain-Driven Design)**. A proposta é fornecer uma experiência visual simples e eficiente para gestão financeira pessoal.

## 🧱 Tecnologias principais
- Arquitetura modular por feature
- Clean Architecture adaptada ao frontend
- Componentes standalone + lazy loading
- Git + GitHub como VCS

## 📁 Estrutura proposta

```
src/app/
├── core/              # Serviços globais, interceptors, guards, baseUrl, etc.
├── shared/            # Componentes, pipes e diretivas reutilizáveis
├── layout/            # Componentes de layout geral (navbar, sidebar, etc)
├── auth/              # Módulo de autenticação
│   ├── domain/        # Entidades, VOs, regras de negócio
│   ├── application/   # Casos de uso (login, registro)
│   ├── infrastructure/# Comunicação com API, localStorage, etc
│   └── presentation/  # Componentes e páginas de interface (login, register)
├── transactions/      # Módulo para movimentações financeiras
└── app.routes.ts      # Rotas standalone do app
```

## 🚀 Como rodar o projeto localmente

```bash
npm install
ng serve
```

Acesse em: [http://localhost:4200](http://localhost:4200)

## 📌 Status atual

- [x] Projeto base criado com Angular 20
- [ ] Estrutura de pastas organizada
- [ ] Autenticação (login)
- [ ] Kanban mensal com transações
- [ ] Responsividade e tema escuro
- [ ] Testes unitários com Jest

## 🧭 Objetivo

Simplificar o controle financeiro mensal, com uma interface que simula uma caderneta visual, focada em fluxo de caixa e agendamento de transações.

---
