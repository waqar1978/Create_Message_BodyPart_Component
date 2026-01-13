# Create Message BodyPart Component

A comprehensive BizTalk Server solution consisting of **two integrated projects**: a custom BizTalk pipeline component and a .NET test application that demonstrates how to use and test the component without requiring a full BizTalk Server deployment.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Solution Architecture](#solution-architecture)
- [Project 1: BizTalk Component](#project-1-biztalk-component)
- [Project 2: .NET Test Application](#project-2-net-test-application)
- [How They Work Together](#how-they-work-together)
- [Quick Start Guide](#quick-start-guide)
- [Detailed Documentation](#detailed-documentation)
- [Requirements](#requirements)
- [Installation & Setup](#installation--setup)
- [Usage Examples](#usage-examples)
- [Testing](#testing)
- [Troubleshooting](#troubleshooting)
- [Project Structure](#project-structure)
- [Technical Deep Dive](#technical-deep-dive)
- [Contributing](#contributing)
- [License](#license)

---

## Project Overview

This repository contains **two complementary projects** that work together:

### 🎯 Project 1: BizTalk Pipeline Component
**Location**: `Create Message BodyPart Component/`  
**Type**: Custom BizTalk Server Pipeline Component (DLL)  
**Purpose**: A reusable BizTalk component that creates message body parts for messages lacking them, ensuring proper XML structure compliance with BizTalk's internal message processing requirements.

**Key Features**:
- ✅ Creates message body parts automatically
- ✅ Configurable XML content via properties
- ✅ Property validation with error logging
- ✅ Null-safe processing (only processes messages without body parts)
- ✅ Ready for production use in BizTalk pipelines

### 🧪 Project 2: .NET Test Application
**Location**: `Create Message BodyPart Component Tests/`  
**Type**: .NET Test Library (NUnit-based)  
**Purpose**: A standalone .NET application that demonstrates how to use and test the BizTalk component **without requiring BizTalk Server installation**.

**Key Features**:
- ✅ Tests the BizTalk component independently
- ✅ Uses Winterdom.BizTalk.PipelineTesting framework
- ✅ Creates test messages programmatically
- ✅ Validates component behavior
- ✅ Demonstrates component usage patterns
- ✅ Can run on any machine with .NET Framework (no BizTalk needed)

---

## Solution Architecture

```
┌─────────────────────────────────────────────────────────────┐
│              Create Message BodyPart Component               │
│                      Solution (.sln)                         │
└─────────────────────────────────────────────────────────────┘
                              │
                ┌─────────────┴─────────────┐
                │                           │
                ▼                           ▼
┌──────────────────────────┐   ┌──────────────────────────┐
│  Project 1:              │   │  Project 2:              │
│  BizTalk Component       │   │  .NET Test Application   │
│                          │   │                          │
│  • Pipeline Component    │   │  • NUnit Test Suite      │
│  • IComponent Interface  │   │  • Uses Component        │
│  • Message Processing    │   │  • Test Helpers          │
│  • Property Management   │   │  • Message Factories     │
│                          │   │  • Assertion Helpers     │
│  Output: DLL             │   │  Output: Test DLL        │
└──────────────────────────┘   └──────────────────────────┘
         │                              │
         │                              │
         └──────────┬───────────────────┘
                    │
                    ▼
         ┌──────────────────────┐
         │  Component Usage     │
         │  & Testing           │
         └──────────────────────┘
```

---

## Project 1: BizTalk Component

### Overview
A custom BizTalk Server pipeline component that solves the common problem of messages arriving without proper body parts. BizTalk Server requires messages to have a specific XML structure, and this component ensures compliance.

### What It Does

**Problem**: 
- BizTalk Server processes messages in a specific XML structure
- Raw XML that doesn't comply causes schema validation failures
- Messages without body parts cannot be processed

**Solution**:
- Automatically creates message body parts when missing
- Converts raw XML strings into BizTalk-compliant format
- Configurable via component properties
- Prevents message parsing failures

### Component Details

**Component Type**: Decoder Pipeline Component  
**Category**: `CATID_Decoder`  
**Stage**: Decode stage of receive pipelines  
**GUID**: `eb908a14-8568-40a8-a32b-4c7f8c02368e`  
**Assembly**: `Modhul.BizTalk.PipelineComponents.CreateMessageBodyPartComp.dll`

### Key Classes

1. **`CreateMessageBodyPartComp.cs`**
   - Main component class implementing BizTalk interfaces
   - Handles message processing logic
   - Property validation and error logging

2. **`MessagePart.cs`**
   - Custom `IBaseMessagePart` implementation
   - Manages message part data and properties

3. **`MessagePartFactory.cs`**
   - Factory for creating message part instances

4. **`PropertyBag.cs`**
   - Property bag implementation for message metadata

5. **`EventLogCategories.cs`**
   - Error code definitions for event logging

### Component Properties

| Property | Type | Required | Description |
|----------|------|----------|-------------|
| `BodyPartName` | String | Yes | Name for the created message body part |
| `BodyPartContent` | String | Yes | XML content for the message body part |

### Usage in BizTalk

1. **Add to Pipeline**:
   - Open receive pipeline in BizTalk Server Administration Console
   - Add component to Decode stage
   - Configure properties

2. **Configure Properties**:
   ```
   BodyPartName: "MyMessageBody"
   BodyPartContent: "<Root xmlns='http://example.com'><Data>Content</Data></Root>"
   ```

3. **Deploy**:
   - Deploy pipeline to BizTalk Server
   - Component processes messages automatically

---

## Project 2: .NET Test Application

### Overview
A comprehensive .NET test application that demonstrates how to use the BizTalk component programmatically. This is particularly valuable because it allows developers to:

- Test the component **without BizTalk Server**
- Understand component usage patterns
- Develop and debug component logic
- Create automated test suites

### What It Does

**Purpose**:
- Tests the BizTalk component in isolation
- Demonstrates component usage patterns
- Validates component behavior
- Provides reusable test utilities

**Key Capabilities**:
- Creates test messages programmatically
- Executes component in test pipeline
- Validates message transformations
- Checks event log entries
- Provides assertion helpers

### Test Framework

**Framework**: NUnit 2.4.8  
**Testing Library**: Winterdom.BizTalk.PipelineTesting 1.1.0  
**Dependencies**: 
- Microsoft.BizTalk.Pipeline.dll
- PipelineObjects.dll (from BizTalk SDK)

### Key Classes

1. **`CreateMessageBodyPartTests.cs`**
   - Main test suite with 4 test scenarios
   - Tests component behavior under various conditions

2. **`MessageCreationFactory.cs`**
   - Creates test messages from resources
   - Creates messages with null body parts
   - Converts streams to BizTalk messages

3. **`AssertionHelpers.cs`**
   - Validates message body parts exist
   - Checks event log entries

4. **`EventLogHelpers.cs`**
   - Manages event log cleanup
   - Checks for error events

5. **`SampleMessage.xml`**
   - Embedded test message resource
   - SOAP envelope example

### Test Scenarios

#### Test 1: Missing BodyPartName Property
```csharp
TestComponentReturnsOriginalMessageWhenBodyPartNamePropertyNotSet()
```
- **Purpose**: Validates error handling when `BodyPartName` is empty
- **Expected**: Message passes through, error logged to Event Log

#### Test 2: Missing BodyPartContent Property
```csharp
TestComponentReturnsOriginalMessageWhenBodyPartContentPropertyNotSet()
```
- **Purpose**: Validates error handling when `BodyPartContent` is empty
- **Expected**: Message passes through, error logged to Event Log

#### Test 3: Message with Existing Body Part
```csharp
TestBodyPartIsNotAddedToMessageWithExistingBodyPart()
```
- **Purpose**: Ensures component doesn't modify messages with existing body parts
- **Expected**: Original message body part preserved

#### Test 4: Message without Body Part
```csharp
TestBodyPartIsAddedToMessageWithoutBodyPart()
```
- **Purpose**: Validates successful body part creation
- **Expected**: New body part created with configured name and content

### Running Tests

```bash
# Using NUnit Console Runner
nunit-console.exe "Create Message BodyPart Component Tests\bin\Debug\Create Message Body Part Component Tests.dll"

# Using Visual Studio Test Explorer
# Right-click solution → Run Tests
```

---

## How They Work Together

### Relationship Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                    Development Workflow                      │
└─────────────────────────────────────────────────────────────┘

1. DEVELOPER WRITES CODE
   ┌─────────────────────┐
   │ BizTalk Component   │ ← Write component logic
   └─────────────────────┘
            │
            │ References
            ▼
   ┌─────────────────────┐
   │ Test Application     │ ← Write tests
   └─────────────────────┘
            │
            │ Executes
            ▼
2. TESTING PHASE
   ┌─────────────────────┐
   │ Test Application    │
   │  • Creates messages │
   │  • Executes comp    │
   │  • Validates output │
   └─────────────────────┘
            │
            │ Tests Pass?
            ▼
3. DEPLOYMENT PHASE
   ┌─────────────────────┐
   │ BizTalk Server      │
   │  • Deploy DLL       │
   │  • Add to Pipeline  │
   │  • Configure Props  │
   └─────────────────────┘
```

### Integration Points

1. **Project Reference**: Test project references the Component project
2. **Component Usage**: Tests instantiate and configure the component
3. **Pipeline Testing**: Uses Winterdom framework to simulate BizTalk pipeline
4. **Message Creation**: Tests create BizTalk messages programmatically
5. **Validation**: Tests verify component behavior matches expectations

### Example: How Test Uses Component

```csharp
// 1. Create component instance
CreateMessageBodyPartComp component = new CreateMessageBodyPartComp();
component.BodyPartName = "SampleBodyPart";
component.BodyPartContent = "<Root>Content</Root>";

// 2. Create test pipeline
ReceivePipelineWrapper pipeline = PipelineFactory.CreateEmptyReceivePipeline();
pipeline.AddComponent(component, PipelineStage.Decode);

// 3. Create test message (without body part)
IBaseMessage inputMessage = MessageCreationFactory.CreateMessageWithNullBodyPart();

// 4. Execute component
MessageCollection outputMessages = pipeline.Execute(inputMessage);

// 5. Validate result
AssertionHelpers.MessageBodyPartIsNotNull(outputMessages[0]);
Assert.AreEqual(outputMessages[0].BodyPartName, "SampleBodyPart");
```

---

## Quick Start Guide

### For Developers New to This Project

#### Step 1: Understand the Projects
- **Component Project**: The actual BizTalk component (what gets deployed)
- **Test Project**: Demonstrates usage and validates functionality

#### Step 2: Explore the Code
1. Start with `CreateMessageBodyPartComp.cs` - main component logic
2. Review `CreateMessageBodyPartTests.cs` - see how it's used
3. Check `MessageCreationFactory.cs` - understand message creation

#### Step 3: Run Tests
```bash
# Build solution first
msbuild "Create Message BodyPart Component.sln"

# Run tests
nunit-console.exe "Create Message BodyPart Component Tests\bin\Debug\*.dll"
```

#### Step 4: Deploy Component (if you have BizTalk)
1. Copy DLL to BizTalk Server
2. Register component
3. Add to pipeline
4. Configure properties

---

## Detailed Documentation

### Component Implementation Details

#### Interfaces Implemented

The component implements four BizTalk interfaces:

1. **`IComponent`**
   - `Execute(IPipelineContext, IBaseMessage)`: Main processing method
   - Processes messages and creates body parts

2. **`IBaseComponent`**
   - `Name`, `Version`, `Description`: Component metadata
   - Retrieved from embedded resources

3. **`IPersistPropertyBag`**
   - `Load(IPropertyBag, int)`: Loads configuration from pipeline
   - `Save(IPropertyBag, bool, bool)`: Saves configuration to pipeline

4. **`IComponentUI`**
   - `Icon`: Component icon in BizTalk Designer
   - `Validate(object)`: Validates configuration at design time

#### Message Processing Flow

```
┌─────────────────────────────────────────────────────────┐
│              Component Execute Flow                     │
└─────────────────────────────────────────────────────────┘

1. Message Received
   └─> IBaseMessage arrives at Execute() method
   
2. Property Validation
   ├─> Check BodyPartName is not empty
   ├─> Check BodyPartContent is not empty
   └─> If invalid → Log error → Return message unchanged
   
3. Body Part Check
   └─> Check if message.BodyPart == null
       ├─> If NOT null → Return message unchanged
       └─> If null → Continue to step 4
       
4. Create Body Part
   ├─> Create IBaseMessagePart via MessagePartFactory
   ├─> Set Data stream from BodyPartContent (Unicode encoding)
   ├─> Set ContentType = "text/xml"
   ├─> Set Charset = "utf-8"
   └─> Add part to message with BodyPartName
   
5. Return Message
   └─> Return modified message
```

#### Encoding Details

- **Content Encoding**: Unicode (UTF-16) byte array
- **Content Type**: `text/xml`
- **Charset**: `utf-8`
- **Stream Type**: `MemoryStream`

### Test Application Details

#### Test Framework Architecture

```
┌─────────────────────────────────────────────────────────┐
│              Test Framework Stack                       │
└─────────────────────────────────────────────────────────┘

NUnit Framework (Test Runner)
    │
    ├─> CreateMessageBodyPartTests (Test Fixture)
    │       │
    │       ├─> Uses Winterdom.BizTalk.PipelineTesting
    │       │       └─> Simulates BizTalk Pipeline
    │       │
    │       ├─> Uses MessageCreationFactory
    │       │       └─> Creates test messages
    │       │
    │       └─> Uses AssertionHelpers
    │               └─> Validates results
    │
    └─> EventLogHelpers
            └─> Manages Windows Event Log
```

#### Message Creation Methods

**From Resource**:
```csharp
IBaseMessage message = MessageCreationFactory.CreateMessageFromResource(
    "Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.Samples.SampleMessage.xml"
);
```

**With Null Body Part**:
```csharp
IBaseMessage message = MessageCreationFactory.CreateMessageWithNullBodyPart();
```

**From Stream**:
```csharp
Stream stream = File.OpenRead("message.xml");
IBaseMessage message = MessageCreationFactory.CreateMessageFromStream(stream);
```

---

## Requirements

### Software Requirements

#### For Component Project
- **BizTalk Server 2006** or later
- **.NET Framework** (compatible with BizTalk version)
- **Visual Studio** (for building)

#### For Test Project
- **.NET Framework** (compatible with BizTalk version)
- **Visual Studio** (for building and running tests)
- **NUnit Framework 2.4.8** or compatible
- **Winterdom.BizTalk.PipelineTesting 1.1.0**
- **BizTalk SDK** (for PipelineObjects.dll - only needed for testing)

### Dependencies

#### Component Dependencies
```
Microsoft.Biztalk.Messaging.dll (v3.0.1.0)
Microsoft.BizTalk.Pipeline.dll (v3.0.1.0)
System.dll
System.Data.dll
System.Drawing.dll
System.Xml.dll
```

#### Test Dependencies
```
Microsoft.BizTalk.Pipeline.dll (v3.0.1.0)
PipelineObjects.dll (v3.0.1.0) - from BizTalk SDK
nunit.framework.dll (v2.4.8.0)
Winterdom.BizTalk.PipelineTesting.dll (v1.1.0.0)
System.dll
System.Data.dll
System.Xml.dll
```

### System Requirements

- **Windows Server** (for BizTalk deployment)
- **Windows** (for development and testing)
- **Sufficient disk space** for BizTalk Server (if deploying)

---

## Installation & Setup

### Building the Solution

#### Step 1: Clone Repository
```bash
git clone <repository-url>
cd Create_Message_BodyPart_Component
```

#### Step 2: Update References (if needed)

**Component Project**:
- Verify BizTalk DLL paths in `.csproj` file
- Default path: `C:\Program Files\Microsoft BizTalk Server 2006\`

**Test Project**:
- Verify Winterdom.BizTalk.PipelineTesting path
- Default path: `C:\Program Files\Winterdom\bin\`
- Verify PipelineObjects.dll path
- Default path: `C:\Program Files\Microsoft BizTalk Server 2006\SDK\Utilities\PipelineTools\`

#### Step 3: Build Solution

**Using Visual Studio**:
1. Open `Create Message BodyPart Component.sln`
2. Right-click solution → Build Solution
3. Check `bin\Debug\` or `bin\Release\` folders

**Using MSBuild**:
```bash
msbuild "Create Message BodyPart Component.sln" /p:Configuration=Debug
```

#### Step 4: Verify Output

**Component Output**:
```
Create Message BodyPart Component\bin\Debug\
  └─ Modhul.BizTalk.PipelineComponents.CreateMessageBodyPartComp.dll
  └─ Modhul.BizTalk.PipelineComponents.CreateMessageBodyPartComp.pdb
```

**Test Output**:
```
Create Message BodyPart Component Tests\bin\Debug\
  └─ Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.dll
```

### Deploying Component to BizTalk

#### Step 1: Copy DLL
```bash
# Copy to BizTalk installation directory or GAC
copy "Create Message BodyPart Component\bin\Debug\Modhul.BizTalk.PipelineComponents.CreateMessageBodyPartComp.dll" 
     "C:\Program Files\Microsoft BizTalk Server 2006\Pipeline Components\"
```

#### Step 2: Register Component
- Component should appear automatically in BizTalk Pipeline Designer
- If not, may need to register in GAC using `gacutil.exe`

#### Step 3: Add to Pipeline
1. Open BizTalk Server Administration Console
2. Create or edit a receive pipeline
3. Add component to Decode stage
4. Configure properties (see Configuration section)

---

## Usage Examples

### Example 1: Basic Component Usage

**Scenario**: Create a body part with simple XML content

```csharp
// In BizTalk Pipeline Configuration
BodyPartName: "MessageBody"
BodyPartContent: "<Root xmlns='http://example.com'><Data>Hello World</Data></Root>"
```

**Result**: Messages without body parts will get a body part named "MessageBody" with the specified XML content.

### Example 2: SOAP Message Body Part

**Scenario**: Create a SOAP-compliant body part

```csharp
BodyPartName: "SoapBody"
BodyPartContent: @"<soap:Body xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>
                     <GetItemMarksReturnResponse xmlns='http://www.logistix-software.com/'>
                       <ItemMarksReturnData xmlns='http://ucles.org.uk/schemas/ILMR' />
                     </GetItemMarksReturnResponse>
                   </soap:Body>"
```

### Example 3: Using Component in Test

**Scenario**: Test component programmatically

```csharp
[Test]
public void TestCustomBodyPartCreation()
{
    // Arrange
    CreateMessageBodyPartComp component = new CreateMessageBodyPartComp();
    component.BodyPartName = "CustomBody";
    component.BodyPartContent = "<Custom><Data>Test</Data></Custom>";
    
    ReceivePipelineWrapper pipeline = PipelineFactory.CreateEmptyReceivePipeline();
    pipeline.AddComponent(component, PipelineStage.Decode);
    
    IBaseMessage inputMessage = MessageCreationFactory.CreateMessageWithNullBodyPart();
    
    // Act
    MessageCollection outputMessages = pipeline.Execute(inputMessage);
    
    // Assert
    Assert.IsNotNull(outputMessages[0].BodyPart);
    Assert.AreEqual("CustomBody", outputMessages[0].BodyPartName);
}
```

### Example 4: Error Handling

**Scenario**: Component handles missing properties gracefully

```csharp
// Component configuration with empty BodyPartName
component.BodyPartName = "";
component.BodyPartContent = "<Root>Content</Root>";

// Component behavior:
// 1. Logs error to Windows Event Log (Error ID: 1010)
// 2. Returns message unchanged
// 3. No exception thrown
```

---

## Testing

### Running All Tests

```bash
# Using NUnit Console
nunit-console.exe "Create Message BodyPart Component Tests\bin\Debug\Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.dll"

# Using Visual Studio Test Explorer
# Tests → Run All Tests
```

### Test Coverage

| Test Case | Purpose | Validates |
|-----------|---------|-----------|
| Missing BodyPartName | Error handling | Event log entry, message unchanged |
| Missing BodyPartContent | Error handling | Event log entry, message unchanged |
| Existing Body Part | Null safety | Original body part preserved |
| No Body Part | Core functionality | Body part created successfully |

### Test Utilities

#### Creating Test Messages

```csharp
// From embedded resource
IBaseMessage msg1 = MessageCreationFactory.CreateMessageFromResource(
    "Namespace.Samples.SampleMessage.xml"
);

// With null body part
IBaseMessage msg2 = MessageCreationFactory.CreateMessageWithNullBodyPart();

// From file stream
using (FileStream fs = File.OpenRead("test.xml"))
{
    IBaseMessage msg3 = MessageCreationFactory.CreateMessageFromStream(fs);
}
```

#### Assertions

```csharp
// Check body part exists
AssertionHelpers.MessageBodyPartIsNotNull(message);

// Check event log entry
AssertionHelpers.ErrorMessageIsWrittenToEventLog(
    (int)EventLogCategories.ComponentPropertyValidation.BodyPartNamePropertyNotConfigured
);
```

---

## Troubleshooting

### Common Issues

#### Issue 1: Component Not Appearing in BizTalk Designer

**Symptoms**:
- Component doesn't show in toolbox
- Cannot add to pipeline

**Solutions**:
1. Verify DLL is in correct location
2. Check DLL is registered in GAC
3. Restart BizTalk Server services
4. Verify component GUID matches registration

#### Issue 2: Property Validation Errors

**Symptoms**:
- Event Log entries with error codes 1010 or 1020
- Messages pass through unchanged

**Solutions**:
- **Error 1010**: Set `BodyPartName` property (non-empty string)
- **Error 1020**: Set `BodyPartContent` property (non-empty string)

#### Issue 3: Tests Failing

**Symptoms**:
- NUnit tests fail to run
- Missing assembly references

**Solutions**:
1. Verify all DLL references are correct
2. Check Winterdom.BizTalk.PipelineTesting is installed
3. Ensure BizTalk SDK is installed (for PipelineObjects.dll)
4. Update HintPath in `.csproj` if DLLs are in different locations

#### Issue 4: Message Body Part Not Created

**Symptoms**:
- Component executes but no body part created

**Checklist**:
- ✅ Verify incoming message actually has `null` body part
- ✅ Check component properties are configured
- ✅ Review Event Log for validation errors
- ✅ Ensure component is in Decode stage
- ✅ Verify message reaches the component

### Event Log Reference

**Event Log Source**: `BizTalk Create Message Body Part Component`

| Error Code | Description | Solution |
|------------|-------------|----------|
| 1010 | BodyPartName property not configured | Set BodyPartName property |
| 1020 | BodyPartContent property not configured | Set BodyPartContent property |

### Debugging Tips

#### Debug Component in BizTalk

1. **Build in Debug mode** with symbols
2. **Attach Visual Studio debugger** to BizTalk host process:
   - Debug → Attach to Process
   - Select `BTSNTSvc.exe` (BizTalk host process)
3. **Set breakpoints** in `Execute()` method
4. **Send test message** to trigger breakpoint

#### Debug Tests

1. **Set breakpoints** in test methods
2. **Run tests in Debug mode**
3. **Step through** component execution
4. **Inspect messages** using Visual Studio debugger

---

## Project Structure

```
Create_Message_BodyPart_Component/
│
├── Create Message BodyPart Component.sln          # Solution file
│
├── Create Message BodyPart Component/              # PROJECT 1: BizTalk Component
│   ├── bin/
│   │   └── Debug/
│   │       ├── Modhul.BizTalk.PipelineComponents.CreateMessageBodyPartComp.dll
│   │       └── Modhul.BizTalk.PipelineComponents.CreateMessageBodyPartComp.pdb
│   │
│   ├── EventLog/
│   │   └── EventLogCategories.cs                   # Error code definitions
│   │
│   ├── Factories/
│   │   └── MessagePartFactory.cs                  # Message part factory
│   │
│   ├── MessagePart/
│   │   ├── MessagePart.cs                         # IBaseMessagePart implementation
│   │   └── PropertyBag.cs                         # Property bag implementation
│   │
│   ├── Properties/
│   │   └── AssemblyInfo.cs                        # Assembly metadata
│   │
│   ├── CreateMessageBodyPartComp.cs               # Main component class
│   ├── CreateMessageBodyPartComp.resx             # Component resources
│   └── Create Message Body Part Component.csproj  # Project file
│
├── Create Message BodyPart Component Tests/        # PROJECT 2: Test Application
│   ├── bin/
│   │   └── Debug/
│   │       └── Modhul.BizTalk.PipelineComponents.AddMessageBodyPartComp.Tests.dll
│   │
│   ├── Factories/
│   │   └── MessageCreationFactory.cs              # Test message factory
│   │
│   ├── Helpers/
│   │   ├── AssertionHelpers.cs                   # Test assertion utilities
│   │   └── EventLogHelpers.cs                    # Event log management
│   │
│   ├── Samples/
│   │   └── SampleMessage.xml                      # Embedded test message
│   │
│   ├── Properties/
│   │   └── AssemblyInfo.cs                        # Assembly metadata
│   │
│   ├── CreateMessageBodyPartTests.cs              # Main test suite
│   └── Create Message Body Part Component Tests.csproj
│
└── README.md                                       # This file
```

---

## Technical Deep Dive

### Component Architecture

#### Class Hierarchy

```
CreateMessageBodyPartComp
    ├── Implements IComponent
    │       └── Execute(IPipelineContext, IBaseMessage)
    │
    ├── Implements IBaseComponent
    │       └── Name, Version, Description
    │
    ├── Implements IPersistPropertyBag
    │       ├── Load(IPropertyBag, int)
    │       └── Save(IPropertyBag, bool, bool)
    │
    └── Implements IComponentUI
            ├── Icon
            └── Validate(object)
```

#### Message Processing Algorithm

```csharp
public IBaseMessage Execute(IPipelineContext context, IBaseMessage message)
{
    // Step 1: Validate properties
    if (!ComponentPropertiesAreValid())
        return message; // Return unchanged
    
    // Step 2: Check if message needs body part
    if (MessageBodyPartIsNull(message))
    {
        // Step 3: Create body part
        IBaseMessagePart bodyPart = CreateMessagePartFromBodyPartContentProperty();
        
        // Step 4: Add to message
        message.AddPart(bodyPartName, bodyPart, true);
    }
    
    // Step 5: Return message
    return message;
}
```

#### Property Validation Logic

```csharp
private bool ComponentPropertiesAreValid()
{
    // Validate BodyPartName
    if (string.IsNullOrWhiteSpace(bodyPartName))
    {
        EventLog.WriteEntry(EVENT_LOG_SOURCE, 
            "BodyPartName property not configured", 
            EventLogEntryType.Error, 
            (int)EventLogCategories.ComponentPropertyValidation.BodyPartNamePropertyNotConfigured);
        return false;
    }
    
    // Validate BodyPartContent
    if (string.IsNullOrWhiteSpace(bodyPartContent))
    {
        EventLog.WriteEntry(EVENT_LOG_SOURCE, 
            "BodyPartContent property not configured", 
            EventLogEntryType.Error, 
            (int)EventLogCategories.ComponentPropertyValidation.BodyPartContentPropertyNotConfigured);
        return false;
    }
    
    return true;
}
```

### Test Framework Architecture

#### Pipeline Testing Flow

```
┌─────────────────────────────────────────────────────────┐
│         Winterdom Pipeline Testing Flow                  │
└─────────────────────────────────────────────────────────┘

1. Create Empty Pipeline
   ReceivePipelineWrapper pipeline = PipelineFactory.CreateEmptyReceivePipeline();

2. Add Component
   pipeline.AddComponent(component, PipelineStage.Decode);

3. Create Test Message
   IBaseMessage message = MessageCreationFactory.CreateMessageWithNullBodyPart();

4. Execute Pipeline
   MessageCollection results = pipeline.Execute(message);

5. Validate Results
   AssertionHelpers.MessageBodyPartIsNotNull(results[0]);
```

#### Message Creation Internals

```csharp
public static IBaseMessage CreateMessageWithNullBodyPart()
{
    // Create message factory
    IBaseMessageFactory factory = new MessageFactory();
    
    // Create empty message
    IBaseMessage message = factory.CreateMessage();
    
    // Create message context
    message.Context = factory.CreateMessageContext();
    
    // Note: BodyPart is null by default
    return message;
}
```

### Encoding and Stream Handling

#### Content Encoding Process

```csharp
private static Stream CreateStreamFromString(string content)
{
    // Convert string to Unicode byte array
    byte[] contentByteArray = Encoding.Unicode.GetBytes(content);
    
    // Create memory stream
    return new MemoryStream(contentByteArray);
}
```

**Important Notes**:
- Uses `Encoding.Unicode` (UTF-16) for byte conversion
- Content type set to `"text/xml"`
- Charset set to `"utf-8"`
- Stream is seekable (`MemoryStream`)

---

## Contributing

### Getting Started

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/your-feature-name`
3. **Make your changes**
4. **Add tests** for new functionality
5. **Update documentation** (README.md)
6. **Submit a pull request**

### Code Style Guidelines

- Follow C# coding conventions
- Use XML documentation comments for public members
- Maintain consistent naming conventions
- Add appropriate error handling and logging
- Write unit tests for new features

### Testing Requirements

- All new features must have corresponding tests
- Tests should cover both success and failure scenarios
- Maintain or improve test coverage
- Tests must pass before submitting PR

### Documentation

- Update README.md for new features
- Add code comments for complex logic
- Document breaking changes
- Update examples if needed

---

## License

Copyright © Modhul (http://www.modhul.com)

This project is provided as-is for use with BizTalk Server.

**Version**: 0.1.0.0  
**Last Updated**: 2024  
**BizTalk Server Compatibility**: 2006 and later

---

## Additional Resources

### Related Documentation

- [BizTalk Server Pipeline Components](https://docs.microsoft.com/en-us/biztalk/core/pipeline-components)
- [Winterdom.BizTalk.PipelineTesting](https://github.com/Winterdom/BizTalk)
- [NUnit Documentation](https://docs.nunit.org/)

### Support

For issues, questions, or contributions:
- **Website**: http://www.modhul.com
- **Repository**: [GitHub Repository URL]

---

## Quick Reference

### Component Properties Quick Reference

| Property | Example Value |
|----------|---------------|
| `BodyPartName` | `"MessageBody"` |
| `BodyPartContent` | `"<Root>Content</Root>"` |

### Error Codes Quick Reference

| Code | Meaning |
|------|---------|
| 1010 | BodyPartName not configured |
| 1020 | BodyPartContent not configured |

### Key Files Quick Reference

| File | Purpose |
|------|---------|
| `CreateMessageBodyPartComp.cs` | Main component |
| `CreateMessageBodyPartTests.cs` | Test suite |
| `MessageCreationFactory.cs` | Test message creation |
| `EventLogCategories.cs` | Error code definitions |

---

**Happy Coding! 🚀**
