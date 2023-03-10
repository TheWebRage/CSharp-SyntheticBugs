// Copyright (c) Umbraco.
// See LICENSE for more details.

using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema.Generation;
using Umbraco.Cms.Core.Configuration.Models;

namespace JsonSchema;

/// <summary>
///     Generator of the JsonSchema for AppSettings.json including A specific Umbraco version.
/// </summary>
public class UmbracoJsonSchemaGenerator
{
    private static readonly HttpClient s_client = new();
    private readonly JsonSchemaGenerator _innerGenerator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UmbracoJsonSchemaGenerator" /> class.
    /// </summary>
    public UmbracoJsonSchemaGenerator()
        => _innerGenerator = new JsonSchemaGenerator(new UmbracoJsonSchemaGeneratorSettings());

    /// <summary>
    ///     Generates a json representing the JsonSchema for AppSettings.json including A specific Umbraco version..
    /// </summary>
    public async Task<string> GenerateMainFile()
    {
        JObject officialSchema = await GetOfficialAppSettingsSchema();
        JObject externalFilePoints = GenerateSchemaWithExternalDefinitions();

        officialSchema.Merge(externalFilePoints);

        return officialSchema.ToString();
    }


    /// <summary>
    /// Generates the CMS file
    /// </summary>
    /// <returns></returns>
    public Task<string> GenerateCmsFile()
    {
        JObject cmsSchema = GenerateUmbracoSchema();

        return Task.FromResult(cmsSchema.ToString());
    }



    private JObject GenerateSchemaWithExternalDefinitions()
    {
        var fileProvider = new EmbeddedFileProvider(GetType().Assembly);

        IFileInfo schema = fileProvider.GetFileInfo("appsettings-schema.json");

        using (Stream? stream = schema.CreateReadStream())
        using (var reader = new StreamReader(stream))
        {
            return JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd())!;
        }
    }

    private async Task<JObject> GetOfficialAppSettingsSchema()
    {
        HttpResponseMessage response = await s_client.GetAsync("https://json.schemastore.org/appsettings.json")
            .ConfigureAwait(false);

        var result = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<JObject>(result)!;
    }

    private JObject GenerateUmbracoSchema()
    {
        NJsonSchema.JsonSchema schema = _innerGenerator.Generate(typeof(AppSettings));

        // TODO: when the "UmbracoPath" setter is removed from "GlobalSettings" (scheduled for V12), remove this line as well
            schema.Definitions["UmbracoCmsCoreConfigurationModelsGlobalSettings"]?.Properties?.Remove(nameof(GlobalSettings.UmbracoPath));return JsonConvert.DeserializeObject<JObject>(schema.ToJson())!;
    }
}
