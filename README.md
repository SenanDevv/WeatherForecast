# Weather Forecast Microservice

This repository contains a .NET Core microservice that provides weather forecasts through a single API endpoint.

## Overview

The microservice accepts input in the form of a date, city, and country, and returns a collection of weather forecasts. The data is obtained via integration with third-party APIs, and the service caches the responses to avoid unnecessary external API calls.

### Key Features

- **Single API Endpoint**: Fetch weather forecasts based on date, city, and country.
- **API Integration**: Retrieves data from third-party weather services.
- **Caching**: Caches API responses to minimize redundant calls to third-party services.
- **Geocoding Support**: Utilizes an external API for geocoding (latitude and longitude) based on the provided city and country.
- **Azure Deployment**: Deployed on Azure Free Tier for easy accessibility.
- **Postman Collection Provided**: A Postman collection is included for easy API testing.

## Usage Instructions

### Request Format

Send a `GET` request to the following URL with the appropriate parameters:

https://weatherforecastapp-g6crh2eqg6b3fca5.westeurope-01.azurewebsites.net/api/WeatherForecasts?date=2024-01-13&country=England&city=London

#### Parameters:

- `date`: The date for the forecast (e.g., `2024-01-13`).
- `city`: The name of the city (e.g., `London`).
- `country`: The name of the country (e.g., `England`).

### Response

The response will contain a collection of weather forecasts for the specified location and date.

### Response

The response will contain a collection of weather forecasts for the specified location and date.

#### Example Response

```json
{
    "success": true,
    "error": null,
    "data": [
        3.3,
        3.33,
        3
    ],
    "statusCode": 0
}
```
### Note

- **Historical Forecasts**: It is recommended to request historical forecasts (e.g., `2024-01-13`) since some future forecasts are provided only through paid API calls. If a future forecast is requested and one of the APIs does not respond properly, the value will be `null`.

## Deployment

The application is deployed on the Azure Free Tier. You can access it through the provided endpoint.

## API Integration Details

The microservice integrates with the following external APIs:

1. **Geocoding API**: To obtain the latitude and longitude of the specified location (city and country).
2. **Weather APIs**: To fetch weather data.

## Caching

The service caches responses to reduce unnecessary third-party API calls, improving performance and minimizing costs.

## Postman Collection

A Postman collection is included in this repository for testing purposes. Import the collection into Postman and use it to test the API.

---

**Disclaimer**: This application uses free-tier services which may have limitations. Performance and availability are subject to change based on third-party service policies.
