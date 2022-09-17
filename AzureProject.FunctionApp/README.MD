# AzureProject.FunctionApp

# Dependacies
https://www.nuget.org/packages/Microsoft.Azure.Functions.Extensions/
Install-Package Microsoft.Azure.Functions.Extensions

https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/
Install-Package Microsoft.Extensions.DependencyInjection

https://www.nuget.org/packages/MongoDB.Driver
Install-Package MongoDB.Driver

# Azure Functions – Time Trigger (CRON) Cheat Sheet
https://arminreiter.com/2017/02/azure-functions-time-trigger-cron-cheat-sheet/
*/15 * * * * * every 15 seconds
0 * * * * * every minute
0 */5 * * * * every 5 minutes
0 */15 * * * * every 15 minutes
0 */30 * * * * every 30 minutes
0 0 * * * *    every hour (hourly)
0 0 0 * * * every day (daily)

# Swagger UI
https://func-app-test-eastus.azurewebsites.net/api/swagger/ui#/

http://localhost:7105/api/swagger/ui#/