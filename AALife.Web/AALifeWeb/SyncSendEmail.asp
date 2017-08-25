<% @ LANGUAGE = VBSCRIPT %>
<%

'*************************************************
'*                                               *
'*   Produced by Dimac                           *
'*                                               *
'*   More examples can be found at 				 *
'*   http://tech.dimac.net                       *
'*                                               *
'*   Support is available at our helpdesk        *
'*   http://support.dimac.net                    *
'*                                               *
'*   Our main website is located at              *
'*   http://www.dimac.net                        *
'*                                               *
'*************************************************
    
	' 接收参数
	Dim mSubject
	mSubject = Request.QueryString("subject")
	'Response.Write(mSubject)
    
    Dim mBody
	mBody = Request.QueryString("body")
	'Response.Write(mBody)

    Dim mEmail
	mEmail = Request.QueryString("email")
	'Response.Write(mEmail)

	' Create the JMail message Object
	set msg = Server.CreateOBject( "JMail.Message" )

	' Set logging to true to ease any potential debugging
	' And set silent to true as we wish to handle our errors ourself
	msg.Logging = true
	msg.silent = True
    msg.Charset = "utf-8"
    msg.ContentType = "text/html; charset='utf-8'"

	' Most mailservers require a valid email address
	' for the sender
	msg.From = "pyfxl@163.com"
	msg.FromName = "AA生活记账"
	
    msg.ReplyTo = mEmail
	
	' Next we have to add some recipients.
	' The addRecipients method can be used multiple times.
	' Also note how we skip the name the second time, it
	' is as you see optional to provide a name.
	'msg.AddRecipient "recipient@hisDomain.com", "His Name"
	msg.AddRecipient "67936108@qq.com"	
	
	' The subject of the message
	msg.Subject = mSubject

	msg.MailServerUserName = "pyfxl"
	msg.MailServerPassword = "7459235sss"

	' The body property is both read and write.
	' If you want to append text to the body you can
	' use JMail.Body = JMail.Body & "Hello world! "
	' or you can use JMail.AppendText "Hello World! "
	' which in many cases is easier to use.
	'
	' Note the use of vbCrLf to add linebreaks to our email
	msg.HTMLBody = mBody

	' There.. we have now succesfully created our message. 
	' Now we can either send the message or save it as a draft in a Database.
	' To save the message you would typicly use the Message objects Text property
	' to do something like this:
	'
	' SaveMessageDraft( msg.Text )
	' Note that this function call is only an example. The function does not
	' exist by default, you have to create it yourself.
	
	
	' If i would like to send my message, you use the Send() method, which
	' takes one parameter that should be your mailservers address
	'
	' To capture any errors which might occur, we wrap the call in an IF statement
	
    flag = msg.Send( "smtp.163.com" )
    msg.Close()

    if flag then
        Response.Write("{ ""result"":""ok"" }")
    else
        Response.Write("{ ""result"":""error"" }")
    end if
    
    Response.End	
	
	' And we're done! the message has been sent.


%>
