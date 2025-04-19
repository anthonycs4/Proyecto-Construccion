terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 4.0.0"
    }
  }
  required_version = ">= 0.14.9"
}

variable "suscription_id" {
  type        = string
  description = "abc8ac6d-2d49-484e-a3f4-dbbdfbe4d933"
}

variable "sqladmin_username" {
  type        = string
  description = "jeanvalverde"
}

variable "sqladmin_password" {
  type        = string
  description = "valverde24c++"
}

provider "azurerm" {
  features {}
  subscription_id = var.suscription_id
}

# FRONTEND DEPLOYMENT
resource "azurerm_resource_group" "frontend_rg" {
  name     = "rg-frontend-valverde-cano"
  location = "brazilsouth"
}

resource "azurerm_service_plan" "frontend_plan" {
  name                = "asp-frontend-valverde-cano"
  location            = azurerm_resource_group.frontend_rg.location
  resource_group_name = azurerm_resource_group.frontend_rg.name
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "frontend_app" {
  name                = "app-front-valverde-cano"
  location            = azurerm_resource_group.frontend_rg.location
  resource_group_name = azurerm_resource_group.frontend_rg.name
  service_plan_id     = azurerm_service_plan.frontend_plan.id

  site_config {
    application_stack {
      docker_image_name   = "greenvalve/turismo-frontend:latest"
      docker_registry_url = "https://index.docker.io"
    }
    minimum_tls_version = "1.2"
    always_on           = false
  }

  https_only = true
}

# BACKEND DEPLOYMENT
resource "azurerm_resource_group" "backend_rg" {
  name     = "rg-backend-valverde-cano"
  location = "brazilsouth"
}

resource "azurerm_service_plan" "backend_plan" {
  name                = "asp-backend-valverde-cano"
  location            = azurerm_resource_group.backend_rg.location
  resource_group_name = azurerm_resource_group.backend_rg.name
  os_type             = "Linux"
  sku_name            = "F1"
}

resource "azurerm_linux_web_app" "backend_app" {
  name                = "app-back-valverde-cano"
  location            = azurerm_resource_group.backend_rg.location
  resource_group_name = azurerm_resource_group.backend_rg.name
  service_plan_id     = azurerm_service_plan.backend_plan.id

  site_config {
    application_stack {
      docker_image_name   = "greenvalve/turismo-backend:latest"
      docker_registry_url = "https://index.docker.io"
    }
    minimum_tls_version = "1.2"
    always_on           = false
  }

  https_only = true

  app_settings = {
    "ConnectionStrings__DefaultConnection" = "Server=${azurerm_mssql_server.sqlsrv.fully_qualified_domain_name};Database=${azurerm_mssql_database.sqldb.name};User Id=${var.sqladmin_username};Password=${var.sqladmin_password};TrustServerCertificate=True"
  }
}

resource "azurerm_mssql_server" "sqlsrv" {
  name                         = "bd-proyecto-valverde-cano"
  resource_group_name          = azurerm_resource_group.backend_rg.name
  location                     = azurerm_resource_group.backend_rg.location
  version                      = "12.0"
  administrator_login          = var.sqladmin_username
  administrator_login_password = var.sqladmin_password
}

resource "azurerm_mssql_firewall_rule" "sqlaccessrule" {
  name             = "PublicAccess"
  server_id        = azurerm_mssql_server.sqlsrv.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "255.255.255.255"
}

resource "azurerm_mssql_database" "sqldb" {
  name      = "Proyecto"
  server_id = azurerm_mssql_server.sqlsrv.id
  sku_name  = "Free"
}
