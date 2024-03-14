// Sample code from: https://github.com/microsoft/BotFramework-WebChat

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Azure.AI.OpenAI;

namespace Models
{
    public class AOAIResponse<T> {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }
    }
    public class Assistant {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    public class Thread
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class MessageInput
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = null;

        [JsonPropertyName("content")]
        public string Content { get; set; } = null;

        [JsonPropertyName("file_ids")]
        public List<string> FileIds { get; set; } = new();
    }
        public class MessageContentText {
        [JsonPropertyName("value")]
        public string Value { get; set; }

    }
    public class MessageContent {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("text")]
        public MessageContentText Text { get; set; }
        [JsonPropertyName("image_file")]
        public ImageFile ImageFile { get; set; }
    }

    public class File {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }


    public class ImageFile {
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public List<MessageContent> Content { get; set; } = null;

        [JsonPropertyName("file_ids")]
        public List<string> FileIds { get; set; }
    }

    public class ThreadRunInput
    {

        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }
    }

    public class Function {
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("arguments")]
        public string Arguments { get; set; }

    }
    public class ToolCall {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("function")]
        public Function Function { get; set; }
        
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
    public class SubmitToolOutputs {
        [JsonPropertyName("tool_calls")]
        public List<ToolCall> ToolCalls { get; set; }
    }
    public class ToolOutput {
        [JsonPropertyName("tool_call_id")]
        public string ToolCallId { get; set; }
        [JsonPropertyName("output")]
        public string Output { get; set; }
    }
    public class ToolOutputData {
        [JsonPropertyName("tool_outputs")]
        public List<ToolOutput> ToolOutputs { get; set; }
    }

    public class RequiredAction {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("submit_tool_outputs")]
        public SubmitToolOutputs SubmitToolOutputs { get; set; }
    }


    public class ThreadRun
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("required_action")]
        public RequiredAction RequiredAction { get; set; }
    }
    public class ImageGenerationInput
    {
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("n")]
        public int N { get; set; }
    }
    public class ImageGenerationOutput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
    public class ImageGenerationStatusResponse
    {
        [JsonPropertyName("result")]
        public ImageGenerationResult Result { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
    public class ImageGenerationResult
    {
        [JsonPropertyName("data")]
        public List<GeneratedImage> Data { get; set; }
    }
    public class GeneratedImage
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("revised_prompt")]
        public string RevisedPrompt { get; set; }
    }

}