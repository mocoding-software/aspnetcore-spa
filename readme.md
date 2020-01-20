# Mocoding.AspNetCore.Spa

Mocoding.AspNetCore.Spa is a .NET Core library that contains common code to enable single page capabilities (React only). Main features:
- Enable Static Files with one year `cache-control`
- React Server for Development
- Server prerendering for Production (using [Jering.Javascript.NodeJS](https://github.com/JeringTech/Javascript.NodeJS))

## Installation

```bash
dotnet add package Mocoding.AspNetCore.Spa
```

## Usage

```C#
public class Startup
{

  // ... 

  public void ConfigureServices(IServiceCollection services)
  {
    services                
      .AddAppInsightsTelemetry(Configuration) // add telemetry
      .AddSpaRenderer(options => // add spa pre-rendering
        {
          options.BabelPolyfill =
            "https://cdnjs.cloudflare.com/ajax/libs/babel-polyfill/7.7.0/polyfill.min.js";
        });
  }
  
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    // ...
    
    // Add static files with cache-control         
    app.UseStaticFilesWithCache();           

    // ...

    // Add Spa middleware at the end.
    app.UseReactSpa(env);
  }
}

```

Please check https://github.com/mocoding-software/spa-template for a full working example.


## Contributions
Pull requests are welcome. Source code is located in ```src``` folder. Tests are located in ```test``` folder. Please make sure to update tests as appropriate.

## Contact Us

Our website: [https://mocoding.com](https://mocoding.com)

Email: [social@mocoding.com](mailto:social@mocoding.com)

## License
[MIT](https://choosealicense.com/licenses/mit/)
