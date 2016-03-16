# Fluent Mailer
Module for sending emails via smtp with fluent interface

# Configuration

## Add into Web.config/app.config standart system.net/mailSettings configuration. 
For example:
```xml
<system.net>
	<mailSettings>
		<smtp from="qwerty@qwerty.com">
			<network enableSsl="true" host="smtp.qwerty.com" port="587" userName="qwerty@qwerty.com" password="qwertyqwerty" />
		</smtp>
	</mailSettings>
</system.net>
```

## Prepare Fluent Mailer Resolving
For now you have two ways to resolve Fluent Mailer Instance:

1. Register Fluent Mailer dependencies in Unity (using FluentMailer.Unity package)

  ```csharp
  var unityContainer = new UnityContainer();
  unityContainer.RegisterFluentMailerDependencies();
  ```
  Now, you can resolve Fluent Mailer through Unity:
  
  ```csharp
  public class SomeService
  {
      private readonly IFluentMailer _fluentMailer;
	
      public SomeService(IFluentMailer fluentMailer)
      {
          _fluentMailer = fluentMailer;
      }
	
      public void SendMessage()
      {
          // Using _fluentMailer here
      }
  }
  ```
  
2. Resolve Fluent Mailer through a static factory (using FluentMailer.Factory package)

  ```csharp
  var fluentMailer = FluentMailerFactory.Create();
  ```
  Additionally, you can configure resolving Fluent Mailer through a static factory for you dependency injection container.
  Example for NInject:
  ```csharp
  Bind<IFluentMailer>.ToMethod(context => FluentMailerFactory.Create())
  ```

# Using Fluent Mailer

## 1. Create Message
```csharp
var message = _fluentMailer.CreateMessage();
```

## 2. Configure Message Body

### With view

```csharp
var mailSender = message.WithView("~/Views/Mailer/Mail.cshtml");
```

### With view and model
```csharp
var model = new MailModel();
var mailSender = message.WithView("~/Views/Mailer/Mail.cshtml", model);
```

### With view body
```csharp
var mailSender = message.WithViewBody("<html><body>Test message</body></html>");
```
    
## 3. Configure Other Message Properties

### Adding receivers
```csharp
mailSender.WithReceiver("abc@abc.com"); // Adds abc@abc.com to recievers
mailSender.WithReceivers(new [] {"bcd@bcd.com", "cde@cde.com"}); // Adds bcd@bcd.com and cde@cde.com to receivers too
```

### Setting Up Subject
```csharp
mailSender.WithSubject("Mail subject");
```

### Adding attachments
```csharp
var fstream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/1.docx", FileMode.Open);
mailSender.WithAttachment(fstream, "document.docx")
	.WithAttachment("~/book.pdf");
```

## 4. Send Mail

### Synchronously
```csharp
mailSender.Send();
```

### Asynchronously
```csharp
await mailSender.SendAsync();
```
    
# Fluent Interface

You can do all of it in one move
```csharp
await _fluentMailer.CreateMessage()
	.WithViewBody("<html><body>Test message</body></html>")
	.WithReceiver("abc@abc.com")
	.WithReceiver("bcd@bcd.com")
	.WithSubject("Mail subject")
	.WithAttachment("~/book.pdf")
	.SendAsync();
```