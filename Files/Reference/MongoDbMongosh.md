# Download
https://downloads.mongodb.com/compass/mongosh-1.5.4-win32-x64.zip
https://www.mongodb.com/docs/mongodb-shell/run-commands/
https://www.mongodb.com/docs/manual/tutorial/query-documents/

# Connect
.\mongosh.exe "mongodb+srv://cluster0.poubl9q.mongodb.net" --apiVersion 1 --username mdelgert --password ChangeMe

use sample_guides

# Create
db.planets.insertOne({ name: "Earth", orderFromSun: 3 });

# Update
db.planets.updateOne({'name':'Earth'},{$set:{'name':'Earth2'}});

# Delete
db.planets.deleteMany({ name : "Earth2" });

# Query
db.planets.find({});

db.planets.find({name:"Mars"});

# Clear console