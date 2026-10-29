/// <summary>
/// Entry point for the Content Builder project, 
/// which when executed will build content according to the "Content Collection Strategy" defined in the Builder class.
/// </summary>
/// <remarks>
/// Make sure to validate the directory paths in the "ContentBuilderParams" for your specific project.
/// For more details regarding the Content Builder, see the MonoGame documentation: <tbc.>
/// </remarks>

/// LANE NOTES:
/// Video Resources: 
/// https://youtu.be/QB43LgRmdNM?si=je4-yciSh21-Usf_
/// https://www.youtube.com/live/CaxeVVYlGH0?si=uHoY5pOphUXVESNk
/// 
/// Docs: 
/// https://monogame.net/blog/2026-07-15-3.8.5-release-2026/
/// https://monogame.net/blog/2025-11-06-MonoGame-Content-Builder/
/// https://docs.monogame.net/articles/getting_started/content_pipeline/content_builder_project.html?tabs=vscode
/// 

using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.Framework.Content.Pipeline.Builder;

var contentCollectionArgs = new ContentBuilderParams()
{
    Mode = ContentBuilderMode.Builder,
    WorkingDirectory = $"{AppContext.BaseDirectory}../../", // path to where your content folder can be located
    SourceDirectory = "Assets", // Not actually needed as this is the default, but added for reference
    Platform = TargetPlatform.DesktopGL
};
var builder = new Builder();

if (args is not null && args.Length > 0)
{
    builder.Run(args);
}
else
{
    builder.Run(contentCollectionArgs);
}

return builder.FailedToBuild > 0 ? -1 : 0;

public class Builder : ContentBuilder
{
    public override IContentCollection GetContentCollection()
    {
        var contentCollection = new ContentCollection();

        // Import all content in the Assets folder using the default importer for their file type.
        contentCollection.Include<WildcardRule>("*");

        // File types that will simply be copied over and not built by the compiler
        contentCollection.IncludeCopy<WildcardRule>("*.json");
        contentCollection.IncludeCopy<WildcardRule>("*.txt");
        contentCollection.IncludeCopy<WildcardRule>("*.csv");
        contentCollection.IncludeCopy<WildcardRule>("*.ttf");
        contentCollection.IncludeCopy<WildcardRule>("*.otf");

        // Specific files that will be excluded
        contentCollection.Exclude<WildcardRule>("Fonts/*.txt");
        contentCollection.Exclude<WildcardRule>("Fonts/*.zip");
        contentCollection.Exclude<WildcardRule>("Fonts/Template.spritefont");
        contentCollection.Exclude<WildcardRule>("README.txt");

        return contentCollection;
    }
}
