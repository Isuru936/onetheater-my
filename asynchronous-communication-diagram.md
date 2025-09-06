# OneTheater Asynchronous Communication Architecture

## Overview

This diagram illustrates the asynchronous communication patterns within the OneTheater modular monolith architecture, showing how modules communicate through domain events, integration events, and the outbox pattern.

## Architecture Diagram

```mermaid
graph TB
    %% External Systems
    API[API Layer] --> UsersModule[Users Module]
    API --> ShowsModule[Shows Module]

    %% Users Module Components
    subgraph UsersModule [Users Module]
        UsersApp[Users Application Layer]
        UsersDomain[Users Domain Layer]
        UsersInfra[Users Infrastructure Layer]

        UsersApp --> UsersDomain
        UsersDomain --> UsersInfra

        %% Domain Events
        UsersDomain --> |"Raise Domain Events"| UserDomainEvents[User Domain Events<br/>- UserCreatedDomainEvent<br/>- KeycloakCreateUserDomainEvent]

        %% Domain Event Handlers
        UserDomainEvents --> |"MediatR Publish"| UserDomainHandlers[Domain Event Handlers<br/>- UserCreatedDomainEventHandler<br/>- KeycloakCreateUserDomainEventHandler]

        %% Integration Events
        UserDomainHandlers --> |"Publish Integration Event"| UserIntegrationEvents[Integration Events<br/>- UserCreatedIntegrationEvent]

        %% Outbox Pattern
        UsersInfra --> |"SaveChanges"| UsersOutbox[Users Outbox Table]
        UsersOutbox --> |"ProcessOutboxJob<br/>(Quartz)"| UsersOutboxProcessor[Outbox Processor]
        UsersOutboxProcessor --> |"Publish Domain Events"| UserDomainHandlers
    end

    %% Shows Module Components
    subgraph ShowsModule [Shows Module]
        ShowsApp[Shows Application Layer]
        ShowsDomain[Shows Domain Layer]
        ShowsInfra[Shows Infrastructure Layer]

        ShowsApp --> ShowsDomain
        ShowsDomain --> ShowsInfra

        %% Integration Event Consumers
        ShowsInfra --> |"MassTransit Consumer"| ShowsConsumers[Integration Event Consumers<br/>- UserCreatedIntegrationEventConsumer]

        %% Outbox Pattern
        ShowsInfra --> |"SaveChanges"| ShowsOutbox[Shows Outbox Table]
        ShowsOutbox --> |"ProcessOutboxJob<br/>(Quartz)"| ShowsOutboxProcessor[Outbox Processor]
        ShowsOutboxProcessor --> |"Publish Domain Events"| ShowsDomainHandlers[Domain Event Handlers]
    end

    %% Message Broker
    subgraph MessageBroker [Message Broker Infrastructure]
        RabbitMQ[RabbitMQ]
        MassTransit[MassTransit]
        EventBus[EventBus Implementation]

        MassTransit --> RabbitMQ
        EventBus --> MassTransit
    end

    %% Database Layer
    subgraph Database [Database Layer]
        UsersDB[(Users Database)]
        ShowsDB[(Shows Database)]

        UsersInfra --> UsersDB
        ShowsInfra --> ShowsDB
    end

    %% Interceptors
    subgraph Interceptors [EF Core Interceptors]
        PublishInterceptor[PublishDomainEventsInterceptor]
        OutboxInterceptor[InsertOutboxMessagesInterceptor]
    end

    %% Communication Flows
    UserIntegrationEvents --> |"IEventBus.PublishAsync"| EventBus
    EventBus --> |"RabbitMQ Message"| RabbitMQ
    RabbitMQ --> |"Consume Message"| ShowsConsumers

    %% Interceptor Connections
    UsersInfra -.-> |"EF Core Interceptors"| PublishInterceptor
    UsersInfra -.-> |"EF Core Interceptors"| OutboxInterceptor
    ShowsInfra -.-> |"EF Core Interceptors"| PublishInterceptor
    ShowsInfra -.-> |"EF Core Interceptors"| OutboxInterceptor

    %% Styling
    classDef moduleClass fill:#e1f5fe,stroke:#01579b,stroke-width:2px
    classDef eventClass fill:#f3e5f5,stroke:#4a148c,stroke-width:2px
    classDef infraClass fill:#e8f5e8,stroke:#1b5e20,stroke-width:2px
    classDef brokerClass fill:#fff3e0,stroke:#e65100,stroke-width:2px

    class UsersModule,ShowsModule moduleClass
    class UserDomainEvents,UserIntegrationEvents,ShowsConsumers eventClass
    class MessageBroker,Database infraClass
    class RabbitMQ,MassTransit,EventBus brokerClass
```

## Communication Patterns

### 1. Domain Events (Within Module)

- **Trigger**: Domain entities raise domain events during business operations
- **Processing**: EF Core interceptors capture and publish domain events via MediatR
- **Handlers**: Domain event handlers process events within the same module

### 2. Integration Events (Between Modules)

- **Publishing**: Domain event handlers publish integration events to the message broker
- **Transport**: MassTransit with RabbitMQ handles message delivery
- **Consumption**: Other modules consume integration events via MassTransit consumers

### 3. Outbox Pattern

- **Reliability**: Ensures reliable message delivery using transactional outbox
- **Processing**: Quartz jobs periodically process outbox messages
- **Retry**: Failed messages are retried with exponential backoff

## Key Components

### Domain Events

- `UserCreatedDomainEvent`
- `KeycloakCreateUserDomainEvent`

### Integration Events

- `UserCreatedIntegrationEvent`

### Event Handlers

- `UserCreatedDomainEventHandler`
- `KeycloakCreateUserDomainEventHandler`

### Consumers

- `UserCreatedIntegrationEventConsumer`

### Infrastructure

- **Message Broker**: RabbitMQ with MassTransit
- **Outbox**: Database tables for reliable messaging
- **Scheduler**: Quartz for outbox processing
- **Interceptors**: EF Core interceptors for event capture

## Benefits

1. **Loose Coupling**: Modules communicate through well-defined events
2. **Reliability**: Outbox pattern ensures message delivery
3. **Scalability**: Asynchronous processing improves performance
4. **Maintainability**: Clear separation of concerns between modules
5. **Resilience**: Retry mechanisms handle transient failures
