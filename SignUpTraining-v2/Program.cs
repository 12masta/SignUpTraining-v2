using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Playwright;

namespace SignUpTraining_v2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var serviceProvider = new ServiceCollection()
                .AddLogging(cfg => cfg.AddConsole())
                .BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogInformation("Creating playwright");

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            logger.LogInformation("Browser Launched");
            var page = await browser.NewPageAsync();
            logger.LogInformation("Page created");
            await page.GotoAsync("https://xf-buskozdroj.gymos.pl/rezerwacja-zajec/");
            await page.FillAsync("id=Email", "staneko@icloud.com");
            await page.SelectOptionAsync("id=dob_dd", "15");
            await page.SelectOptionAsync("id=dob_mm", "5");
            await page.SelectOptionAsync("id=dob_rr", "1996");
            await page.FillAsync("id=Karta", "4201720201");
            await page.ClickAsync("[class=zaloguj]");
            logger.LogInformation("User logged in");

            await page.ClickAsync("[class='arrow right']");

            var trainingDay = new TrainingWeek().GetTrainingDaySelectors();

            foreach (var selector in trainingDay)
            {
                try
                {
                    logger.LogInformation($"Start operqtion for selector: {selector}");
                    await page.ClickAsync(selector);
                    await page.ClickAsync("id=zapisz");
                    logger.LogInformation("Training saved successfuly");
                    await page.ClickAsync("id=anuluj");
                    await page.ReloadAsync();
                    logger.LogInformation("Reloading website");
                }
                catch (Exception e)
                {
                    logger.LogError($"Training roll up failed with Exception: {e}");
                    var guid = Guid.NewGuid();
                    await page.ScreenshotAsync(new PageScreenshotOptions {Path = "screenshot-failure" + guid + ".png"});
                    await page.ReloadAsync();
                }
            }

            await page.ScreenshotAsync(new PageScreenshotOptions {Path = "screenshot.png"});
        }
    }
}