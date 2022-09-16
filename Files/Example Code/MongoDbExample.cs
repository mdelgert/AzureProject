using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbExample
{

    class Program
    {
        static void Main(string[] args)
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://test_user:MongoDBR0cks@demodata-uymyo.mongodb.net/test?retryWrites=true&w=majority");

            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }

            Console.WriteLine("Connecting to sample_training.grades");

            var database = dbClient.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            // Define a new student for the grade book.

            var document = new BsonDocument
            {
                { "student_id", 10000 },
                { "scores", new BsonArray
                    {
                    new BsonDocument{ {"type", "exam"}, {"score", 88.12334193287023 } },
                    new BsonDocument{ {"type", "quiz"}, {"score", 74.92381029342834 } },
                    new BsonDocument{ {"type", "homework"}, {"score", 89.97929384290324 } },
                    new BsonDocument{ {"type", "homework"}, {"score", 82.12931030513218 } }
                    }
                },
                { "class_id", 480}
            };

            // *********************************
            // Create Operations
            // *********************************

            // Insert the new student grade records into the database.

            Console.WriteLine("Inserting Document");
            collection.InsertOne(document);
            Console.WriteLine("Document Inserted.\n");

            // *********************************
            // Read Operations
            // *********************************

            // Find first record in the database
            var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine("\n**********\n");
            Console.WriteLine(firstDocument.ToString());

            // Find a specific document with a filter
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            Console.WriteLine("\n**********\n");
            Console.WriteLine(studentDocument.ToString());

            // Find all documents with an exam score equal or above 95 as a list
            //var highExamScoreFilter = Builders<BsonDocument>
            //    .Filter.Eq("scores.type", "exam") & Builders<BsonDocument>.Filter.Gte("score", 95);
            var highExamScoreFilter = Builders<BsonDocument>.Filter
                .ElemMatch<BsonValue>("scores",
                new BsonDocument { { "type", "exam" },
                    { "score", new BsonDocument { { "$gte", 95 } } }
                });
            var highExamScores = collection.Find(highExamScoreFilter).ToList();
            Console.WriteLine("\n**********\n");
            Console.WriteLine(highExamScores);

            // Find all documents with an exam score equal
            // or above 95 as an iterable

            var cursor = collection.Find(highExamScoreFilter).ToCursor();
            Console.WriteLine("\n**********\n");
            Console.WriteLine("\nHigh Scores Iterable\n");
            Console.WriteLine("\n**********\n");
            foreach (var cursorDocument in cursor.ToEnumerable())
            {
                Console.WriteLine(cursorDocument);
            }

            // Sort the exam scores by student_id
            var sort = Builders<BsonDocument>.Sort.Descending("student_id");
            var highestScore = collection.Find(highExamScoreFilter).Sort(sort).First();
            Console.WriteLine("\n**********\n");
            Console.WriteLine("\nHigh Score\n");
            Console.WriteLine("\n**********\n");
            Console.WriteLine(highestScore);


            // *********************************
            // Update Operations
            // *********************************

            // Update quiz score.
            Console.WriteLine("\n**********\n");
            Console.WriteLine("Update class_id");
            var quizUpdateFilter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);

            var update = Builders<BsonDocument>.Update.Set("class_id", 483);
            var result = collection.UpdateOne(quizUpdateFilter, update);
            Console.WriteLine(result);

            // Array Updates
            Console.WriteLine("\n**********\n");
            Console.WriteLine("Update score.type.quiz array value");
            var arrayFilter = Builders<BsonDocument>.Filter
                .Eq("student_id", 10000)
                & Builders<BsonDocument>.Filter.Eq("scores.type", "quiz");
            var arrayUpdate = Builders<BsonDocument>.Update
                .Set("scores.$.score", 84.92381029342834);

            var arrayUpdateResult = collection.UpdateOne(arrayFilter, arrayUpdate);
            Console.WriteLine(arrayUpdateResult);

            //// *********************************
            //// Delete Operations
            //// *********************************

            // Delete the student record.
            Console.WriteLine("\n**********\n");
            Console.WriteLine("Deleting the record.");
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
            var deleteResult = collection.DeleteOne(deleteFilter);
            Console.WriteLine(deleteResult);

            // Delete the low exams.
            Console.WriteLine("\n**********\n");
            Console.WriteLine("Deleting the record.");
            var deleteLowExamFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>("scores", new BsonDocument {
                { "type", "exam" }, { "score", new BsonDocument { { "$lt", 60 } }}
            });

            var deleteManyResults = collection.DeleteMany(deleteLowExamFilter);
            Console.WriteLine(deleteManyResults);

        }
    }
}

//https://gist.github.com/kenwalger/4a3da771b8471c43d190327556ebc3ab