// Learn more about F# at http://fsharp.org

open System
open IOMail.Imap
open IOConfig.Ini
open IOConfig.Queries.Mail
open IOConfig.Connection
open System.Collections.Generic
open System.Net.Mail
open System.Linq

let downloadMail count =
  let path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.config/fmailer/config.ini"
  let iniMap = readIniToDict path
  let msgs: IEnumerable<MailMessage> =
    getMessages iniMap.["UNAME"] iniMap.["PASS"] iniMap.["SNAME"]  (iniMap.["PORT"] |> int)
  for msg in msgs.Take(count) do
    insertMail msg.Subject msg.From.Address |> Async.RunSynchronously

let readMail count =
  let rows = selectMail |> Async.RunSynchronously
  for row in rows.Take(count) do
    printfn "title<from>:%s <%s>\n" row.Subject row.FromAddr

let twoArg act count =
  match act with
    | "download" -> downloadMail count
    | "read" -> readMail count
    | _ -> printfn "failed command."

[<EntryPoint>]
let main argv =
  match argv.Length with
    | 2 -> twoArg (argv.First()) (int argv.[1])
    | _ -> printfn "failed length command"
  0
