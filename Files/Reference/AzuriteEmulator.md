https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio#running-azurite-from-the-command-line

https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=npm
Use the Azurite emulator for local Azure Storage development

# Install Azurite
npm install -g azurite

# Run Azurite
azurite --silent --location c:\azurite --debug c:\azurite\debug.log

https://developercommunity.visualstudio.com/t/azure-functions-not-starting-in-vs-with-azurite-if/1155814

Add to function app to start azure storage

<PropertyGroup> 
<StartDevelopmentStorage>true</StartDevelopmentStorage> 
</PropertyGroup>