// Learn more about F# at http://fsharp.org

open System
open IOMail.Imap
open IOConfig.Ini
open System.Collections.Generic
open System.Net.Mail
open System.Linq
open FSharp.Data.Sql

[<Literal>]
let connectionString =
    "Data Source=" +
    __SOURCE_DIRECTORY__ + @"/db.sqlite;" +
    "Version=3;foreign keys=true"

type Database = SqlDataProvider<
  Common.DatabaseProviderTypes.SQLITE,
  SQLiteLibrary = Common.SQLiteLibrary.SystemDataSQLite,
  ConnectionString = connectionString>

let context = Database.GetDataContext()

[<EntryPoint>]
let main argv =
  let iniMap = readIniToDict "config.ini"
  let msgs: IEnumerable<MailMessage> =
    getMessages iniMap.["UNAME"] iniMap.["PASS"] iniMap.["SNAME"]  (iniMap.["PORT"] |> int)
  for msg in msgs do
    printfn "title<from>:%s <%s>\n" msg.Subject msg.From.Address
    let mail = context.Main.Mail.Create()
    mail.Subject <- msg.Subject
    mail.FromAddr <- msg.From.Address
    context.SubmitUpdates()
  0
