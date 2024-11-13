Spendr - Serverless Expense Tracker API
=======================================

Spendr is a serverless API built with Azure Functions to securely store and retrieve expense data using Cosmos DB. This project leverages cloud-native Azure services to provide a highly scalable, secure, and low-maintenance solution.

![dev(spendrfunctionapp)](https://github.com/user-attachments/assets/ac99988d-8d36-4865-b960-714183d2c728)

### **Key Features**

-   **AddExpense and GetExpenses Endpoints**: Handles HTTP requests to add and retrieve expense data.
-   **Azure Cosmos DB**: Scalable NoSQL database that organizes data with a partition key for efficient access.
-   **Azure Key Vault**: Secure storage for sensitive data, such as Cosmos DB connection strings.
-   **Managed Identity**: Enables secure, credential-free access to Key Vault from the Function App.

### **Architecture Overview**

1.  **Azure Functions** handle HTTP requests without the need for managing infrastructure.
2.  **Cosmos DB** stores user-specific expense records efficiently.
3.  **Azure Key Vault** securely stores the Cosmos DB connection string.
4.  **Managed Identity** allows the Function App to access Key Vault securely.

### **Setup and Deployment**

1.  Clone the repository.
2.  Configure Azure resources (Function App, Cosmos DB, Key Vault).
3.  Deploy the Function App and set up Managed Identity and Key Vault access.

### **Blog Posts**

Learn more about the architecture and setup in my series of blog posts:

-   [Building a Serverless Expense Tracker API on Azure](https://freemancodz.hashnode.dev/building-a-serverless-expense-tracker-api-on-azure)
-   [Storing Secrets in Azure Key Vault and Accessing with Managed Identity](https://freemancodz.hashnode.dev/storing-secrets-in-azure-key-vault-and-accessing-with-managed-identity)
-   [Deploying a Function App Using Azure CLI](https://freemancodz.hashnode.dev/deploying-a-function-app-using-azure-cli)
-   [Develop a Simple Expense Tracker API Locally Using Azure Function](https://hashnode.com/65a5d7dade4071414555f079/dashboard/posts#:~:text=Develop%20a%20Simple%20Expense%20Tracker%20API%20Locally%20Using%20Azure%20Function)
