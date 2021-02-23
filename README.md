# Introduction 
Projekt wielowarstowej apliacji internetowej obejmuje symulację aplikacji mailowej. Główną motywacją do stworzenia tego projektu była chęć zdania przedmiotu na studiach.

# Getting Started
Projekt jest podzielony na 5 projektów: 
1. API ("Inlook_Api"). Projekt zawierający API serwera, odbierająca i wysyłająca zapytania REST;
2. Core ("Inlook_Core"). Projekt zawierjący logikę serwera;
3. Infrastructure ("Inlook_Infrastructure"). Projekt modelujący i obsługujący zapytania do bazy danych.
4. ClientApp ("Inlook_app"). Projekt frontendowty odpowiadający aplikacji klienckiej.
5. Tests ("XUnitTests"). Projekt zawierający testy jednostkowe API oraz testy aplikacji; 

Projekty API, Core, Infrastructure, Tests są napisane w technologi .NET Core.
Projet ClientApp jest napisany z użyciem technologi React w języku TypeScript. Do stylizacji został użyty framework Material-UI.

# Database
Do połączenia z bazą danych korzystamy z frameworku EntityFramework Core. 

# Build and Test 
Projekty  API, Core, Infrastructure, Tests należy uruchomić w dowolnym środowisku obsługującym technologię .NET Core.
Projek ClientApp należy uruchomić przy pomocy środowiska NodeJS z włączoną obsługą TypeScript. Np.: korzystając z narzędzia npm należy wywołać: "npm install; npm start;"


# Contribute
Każdy użytkownik może modyfikować kod i dostosowywać go do swoich potrzeb. 

# Authors
Team "JaKubenBogen": 
    Maciej "Bogen" Chlebny
    Artur "Ja" Chmura
    Jakub "Kuben" Królik
