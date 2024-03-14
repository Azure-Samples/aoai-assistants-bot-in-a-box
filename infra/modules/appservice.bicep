param location string
param appServicePlanName string
param appServiceName string
param msiID string
param msiClientID string
param sku string = 'S1'
param tags object = {}
param openaiGPTModel string
param openaiEmbeddingsModel string
param openaiDalleModel string
param speechName string
var speechNames = !empty(speechName) ? [speechName] : []

param openaiName string
param storageName string

param openaiEndpoint string
param cosmosEndpoint string
param speechEndpoint string


resource openai 'Microsoft.CognitiveServices/accounts@2023-05-01' existing = {
  name: openaiName
}

resource speeches 'Microsoft.CognitiveServices/accounts@2023-05-01' existing = [for name in speechNames: {
  name: name
}]

resource storage 'Microsoft.Storage/storageAccounts@2021-09-01' existing = {
  name: storageName
}

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
  tags: tags
  sku: {
    name: sku
  }
}

resource appService 'Microsoft.Web/sites@2022-09-01' = {
  name: appServiceName
  location: location
  tags: union(tags, { 'azd-service-name': 'assistant-bot-app' })
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${msiID}': {}
    }
  }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      http20Enabled: true
      appSettings: [
        {
          name: 'MicrosoftAppType'
          value: 'UserAssignedMSI'
        }
        {
          name: 'MicrosoftAppId'
          value: msiClientID
        }
        {
          name: 'MicrosoftAppTenantId'
          value: tenant().tenantId
        }
        {
          name: 'AOAI_API_ENDPOINT'
          value: openaiEndpoint
        }
        {
          name: 'AOAI_API_KEY'
          value: openai.listKeys().key1
        }
        {
          name: 'AOAI_GPT_MODEL'
          value: openaiGPTModel
        }
        {
          name: 'AOAI_EMBEDDINGS_MODEL'
          value: openaiEmbeddingsModel
        }
        {
          name: 'AOAI_ASSISTANT_ID'
          value: 'YOUR_ASSISTANT_ID'
        }
        {
          name: 'AOAI_DALLE_DEPLOYMENT'
          value: openaiDalleModel
        }
        {
          name: 'SPEECH_API_ENDPOINT'
          value: speechEndpoint
        }
        {
          name: 'SPEECH_API_KEY'
          value: !empty(speechName) ? speeches[0].listKeys().key1 : ''
        }
        {
          name: 'SPEECH_REGION'
          value: !empty(speechName) ? speeches[0].location : ''
        }
        {
          name: 'COSMOS_API_ENDPOINT'
          value: cosmosEndpoint
        }
        {
          name: 'DIRECT_LINE_SECRET'
          value: ''
        }
        {
          name: 'PROMPT_WELCOME_MESSAGE'
          value: 'Welcome to Assistants Bot in-a-box! Ask me anything to get started.'
        }
        {
          name: 'PROMPT_SYSTEM_MESSAGE'
          value: 'Answer the questions as accurately as possible using the provided functions.'
        }
        {
          name: 'PROMPT_SUGGESTED_QUESTIONS'
          value: '[]'
        }
        {
          name: 'SSO_ENABLED'
          value: 'false'
        }
        {
          name: 'SSO_CONFIG_NAME'
          value: ''
        }
        {
          name: 'SSO_MESSAGE_TITLE'
          value: 'Please sign in to continue.'
        }
        {
          name: 'SSO_MESSAGE_PROMPT'
          value: 'Sign in'
        }
        {
          name: 'SSO_MESSAGE_SUCCESS'
          value: 'User logged in successfully! Please repeat your question.'
        }
        {
          name: 'SSO_MESSAGE_FAILED'
          value: 'Log in failed. Type anything to retry.'
        }
        {
          name: 'USE_STEPWISE_PLANNER'
          value: 'true'
        }
        {
          name: 'BLOB_API_ENDPOINT'
          value: 'https://${storageName}.blob.${environment().suffixes.storage}'
        }
        {
          name: 'BLOB_API_KEY'
          value: storage.listKeys().keys[0].value
        }
      ]
    }
  }
}

output appName string = appService.name
output hostName string = appService.properties.defaultHostName
