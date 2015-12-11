# InComm.BuildReader
Reads TeamCity stats and writes them to ElasticSearch.

## Getting Started
* Clone https://github.com/KevinFinckIDS/BuildReader.
* Open the InComm.BuildReader.sln.
* Verify the _TeamCityServerAddress_ address in InComm.BuildReader\Program.cs.
* Verify the _ElasticSearchAddress_ address in InComm.BuildReader\Program.cs if you are going to upload the results.
* Create environment variable _TeamCityUsername_ with your calling login.
* Create environment variable _TeamCityPassword_ with your calling credentials.
* Run the program. _By default it just displays to the console. Uncomment the "UploadTeamCity..." to write to Elasticsearch.
