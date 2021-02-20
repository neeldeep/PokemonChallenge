# TrueLayer Senior Backend Engineer Challenge
This is a simple REST API, which takes a Pokemon name as input and returns the translated Shakespearan Description. By deafult the `Ruby` Version is selected from the Pokemon Species Flavor text for translation.
# API Reference
 - Poke API : https://pokeapi.co/api/v2
 - Shakespeare API: https://api.funtranslations.com/translate/
# Technologies/Frameworks Used
 - DotNetCore 3.1
 - PokeApiNet (https://github.com/mtrdp642/PokeApiNet)
 - Swagger 
 - NUnit
 - Moq
 - Docker
  # Project Structure
  - Pokemon.API: This is the main Web API Project.This has the Pokemon Controller. We also have the custom Exception Middle Filter defined in this project. Pokemon Service has been injected in the Controller.
  - Pokemon.Entities: This project has all the Entities that are being used across the application.
  - Pokemon.Services: The Services are defined in the class library and has all the Business Logic around the application. There are mainly 3 Services in the Project.
    The Poke API Service is a class on top of Poke API .Net library. I created this service as there was no way to inject the Poke API Client. Also this helps in testing the service as we can inject the Poke Client as an dependency.
  - Pokemon.Tests: This project has both Unit Test and Integration Test.
# Running the Project using Docker
 - Download and Install Docker (https://docs.docker.com/get-docker/)
 - If you don't have Git, download and install Git (https://git-scm.com/downloads)
 - Clone the Repo
 - Navigate to the Solution Folder "PokemonChallenge"
 - Run the command `docker build . -t pokemon -f "Pokemon.API\Dockerfile"`. If using non Windows system use, `docker build . -t pokemon -f "Pokemon.API/Dockerfile"`
 - Run the command `docker images` to see the `pokemon` Docker image
 - Run the command `docker run --rm -p 5000:80 --name pokemonapi pokemon` Pokemonapi will be the container name. Local Port 5000 is mapped to container port 80
 - Navigate to `http://localhost:5000/swagger`
 - You can use Pokemon API from Swagger Interface
 - or Directly call the API: `http://localhost:5000/pokemon/pikachu`
# Running the Project using Visual Studio
 - Ensure you have Visual Studio 2019 Installed
 - Download and Install Dot Net Core 3.1 (https://dotnet.microsoft.com/download/dotnet/3.1)
 - Open `PokemonChallenge.sln` in Visual Studio
 - Build the Solution
 - Open Test Explorer Window in Visual Studio and run the Tests
 - Also you can run the Web API Project using IIS Express within Visual Studio if using Windows
 - You can also use dotnet command from Test folder to run tests. Naviagte to `PokemonChallenge\Pokemon.Tests` and run `dotnet test`
 # Sample Request & Response
 ```
 Request: http://localhost:5000/pokemon/pikachu
 Response:
 {
   "name":"pikachu",
  "description":"At which hour several of these pokémon gather,  their electricity couldst buildeth and cause lightning storms."
 }

 Request: http://localhost:5000/pokemon/invalidPokemonName
 Response:
 Invalid Pokemon Name

 Request: http://localhost:5000/pokemon/ditto
 Response:
 No Shakespeare Translation Available as this time. Please try again later
 ```
 # Key Highlights
 - Async & Await for the external API calls.
 - For the translation part, I have registerd the Translation Service as an HttpClient.
 - Have created a `Dependencies.cs` to register the dependecies. It also keeps the `Startup.cs` clean.
 - I have created wrapper class on top the Poke API .Net Client. This is to inject the Poke API Client within the Pokemon Service.
 - I have registered a Custom Exception Middleware Filter to handle any Exception within the application.
 - There is custom Exception Class (APIException). This is used to customize the Exception Messages in certain scenarios and let the consumer know why that exception might have      occured. For e.g. a consumer might have mistakenly provided an in valid pokemon name so the Exception will let the consumer know about this.
	Also there is a Rate Limit on the Shakespeare API. So it will let the consumer know to try after some time.
 - Swagger has been setup to provide UI and documentation around the API.

 # Areas of Improvement
  - For this solution, the Language and Version Name(`Ruby`) are hard coded. This can be user driven and can also be setup in the configuration.
  - Implement caching against each Pokemon Name. Since the Translate API has a limit on number of calls, this will improve user experience.
  - We can avoid using the Poke API .Net client and directly call the API if we want to avoid adding additional Nuget dependencies to the project.


 



