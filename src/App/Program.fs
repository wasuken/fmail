// Learn more about F# at http://fsharp.org

open System
open Mail.Imap
open System.Collections.Generic
open System.Net.Mail
open System.Linq

[<EntryPoint>]
let main argv =
  let msgs:IEnumerable<MailMessage> = getMessages "hoge" "hoge" "hoge" 993
  let formattedMsgList = msgs.Select(fun msg -> (printfn "%s <%s>\nbody:\n%s" msg.Subject msg.From.Address msg.Body))
  Console.WriteLine("{0}", System.String.Join("\n", formattedMsgList))
  0
