﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.SemanticKernel.TextGeneration;
using OllamaSharp;
using Xunit;

namespace SemanticKernel.Connectors.Ollama.UnitTests.Extensions;

/// <summary>
/// Unit tests of <see cref="OllamaServiceCollectionExtensions"/>.
/// </summary>
public class OllamaServiceCollectionExtensionsTests
{
    [Fact]
    public void AddOllamaTextGenerationToServiceCollection()
    {
        var services = new ServiceCollection();
        services.AddOllamaTextGeneration("model", new Uri("http://localhost:11434"));

        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<ITextGenerationService>();

        Assert.NotNull(service);
        Assert.IsType<OllamaTextGenerationService>(service);
    }

    [Fact]
    public void AddOllamaChatCompletionToServiceCollection()
    {
        var services = new ServiceCollection();
        services.AddOllamaChatCompletion("model", new Uri("http://localhost:11434"));

        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<IChatCompletionService>();

        Assert.NotNull(service);
    }

    [Fact]
    public void AddOllamaChatCompletionFromServiceCollection()
    {
        var services = new ServiceCollection();
        using var ollamaClient = new OllamaApiClient(new Uri("http://localhost:11434"), "model");

        services.AddSingleton(ollamaClient);
        services.AddOllamaChatCompletion();
        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<IChatCompletionService>();
        Assert.NotNull(service);
    }

    [Fact]
    public void AddOllamaTextEmbeddingGenerationFromServiceCollection()
    {
        var services = new ServiceCollection();
        using var ollamaClient = new OllamaApiClient(new Uri("http://localhost:11434"), "model");

        services.AddSingleton(ollamaClient);
        services.AddOllamaTextEmbeddingGeneration();
        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<ITextEmbeddingGenerationService>();
        Assert.NotNull(service);
    }

    [Fact]
    public void AddOllamaTextEmbeddingsGenerationToServiceCollection()
    {
        var services = new ServiceCollection();
        services.AddOllamaTextEmbeddingGeneration("model", new Uri("http://localhost:11434"));

        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<ITextEmbeddingGenerationService>();

        Assert.NotNull(service);
    }
}
