# update-trial

Steps: <br>
1. Create github action workflow
In the command dont add runtime to make a smaller zip (~100kb) else if runtime is --win-x64 then it will be 70MB

2. The action workflow runs when you push a tag. To push a tag
    1. git tag v0.x.y
    2. git push origin v0.x.y
NOTE: the .yml file within this tag will be used to run the github actions workflow.

Now you can find the zip files within your releases tab in github
