# Links

https://github.com/marketplace/actions/publish-nuget
https://garywoodfine.com/how-to-use-github-actions-to-build-deploy-github-nuget-packages/
https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry
https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token
https://blog.codecentric.de/en/2020/07/nuget-packages-github/
https://github.com/codecentric/net_core_admin
https://docs.github.com/en/packages
https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token
https://docs.github.com/en/packages/learn-github-packages/viewing-packages
https://stackoverflow.com/questions/57889719/how-to-push-nuget-package-in-github-actions
https://github.com/acraven/blog-nuget-workflow-github-actions
https://acraven.medium.com/a-nuget-package-workflow-using-github-actions-7da8c6557863
https://stackoverflow.com/questions/18216991/create-a-tag-in-a-github-repository
https://devconnected.com/how-to-delete-local-and-remote-tags-on-git/
https://josef.codes/dotnet-pack-include-referenced-projects/

# Example

dotnet new console --name OctocatApp
cd OctocatApp
dotnet pack --configuration Debug
dotnet nuget push "OctocatApp/bin/Debug/NugetExample.1.0.0.nupkg" --source "github"

# Auto action from tag for workflow to run tags must start with v

git tag v1.0.0
git push --tags