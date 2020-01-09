// Learn more about F# at http://fsharp.org

open System
open IOMail.Imap
open IOConfig.Ini
open System.Collections.Generic
open System.Net.Mail
open System.Linq

[<EntryPoint>]
let main argv =
  let iniMap = readIniToDict "config.ini"
  let msgs: IEnumerable<MailMessage> =
    getMessages iniMap.["UNAME"] iniMap.["PASS"] iniMap.["SNAME"]  (iniMap.["PORT"] |> int)
  for msg in msgs.Take(10) do
    printfn "title<from>:%s <%s>\n" msg.Subject msg.From.Address
  0
