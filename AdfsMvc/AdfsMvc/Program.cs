using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.WsFederation;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddAuthentication( sharedOptions =>
       {
           sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
           sharedOptions.DefaultChallengeScheme = WsFederationDefaults.AuthenticationScheme;
       } )
       .AddWsFederation( options =>
       {
           options.Wtrealm = builder.Configuration["wsfed:realm"]; // TODO set your config options!
           options.MetadataAddress = builder.Configuration["wsfed:metadata"];

           // options.TokenHandlers.Clear(); options.TokenHandlers.Add(new CustomSamlSecurityTokenHandler());
       } )
       .AddCookie();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler( "/Home/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}" );
app.MapRazorPages();

app.Run();

/*
public class CustomSamlSecurityTokenHandler : SamlSecurityTokenHandler
{
    public override async Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
    {

        var configuration = await validationParameters.ConfigurationManager.GetBaseConfigurationAsync(CancellationToken.None).ConfigureAwait(false);
        var issuers = new[] { configuration.Issuer };
        validationParameters.ValidIssuers = (validationParameters.ValidIssuers == null ? issuers : validationParameters.ValidIssuers.Concat(issuers));
        validationParameters.IssuerSigningKeys = (validationParameters.IssuerSigningKeys == null ? configuration.SigningKeys : validationParameters.IssuerSigningKeys.Concat(configuration.SigningKeys));

        var result =  await base.ValidateTokenAsync(token, validationParameters);

        return result;
    }
}
*/