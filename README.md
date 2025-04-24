# Project: HackerNews API Integration (.NET 8)

This project is built with **.NET Core 8** and exposes three REST API endpoints that internally call the official [Hacker News API](https://github.com/HackerNews/API). It includes CORS configuration for cross-origin access.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- IDE such as [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

## Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/your-repo-name.git
   cd your-repo-name
   ```

2. **Add CORS Policy**
   In `Program.cs` or `Startup.cs`, add the following:
   ```csharp
   builder.Services.AddCors(options =>
   {
       options.AddPolicy("AllowSpecificOrigin",
           policy => policy.WithOrigins("https://your-frontend-url.com")
                            .AllowAnyHeader()
                            .AllowAnyMethod());
   });

   app.UseCors("AllowSpecificOrigin");
   ```

3. **Run the Application**
   ```bash
   dotnet run
   ```

---

## REST API Endpoints

These APIs internally call the Hacker News API:

1. **Top Stories**
   - `GET /api/news/top`
   - Fetches top story IDs and details.

2. **New Stories**
   - `GET /api/news/new`
   - Retrieves the latest stories.

3. **Story by ID**
   - `GET /api/news/story/{id}`
   - Returns a specific story based on the provided ID.

---

## Additional Notes

- Ensure that the backend has internet access to fetch data from HackerNews.
- CORS policy should be adjusted according to your production frontend domain.
- Logging and error handling can be extended as needed.

---

## License

MIT License

---

## Contact

For any issues, please open an issue on the GitHub repository.

