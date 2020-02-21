namespace IOConfig

open System
open System.IO
open System.Linq
open System.Collections.Generic
open FSharp.Data.Dapper
open Microsoft.Data.Sqlite

module Ini =
  let readIniToDict (path: string) =
    let mutable dict = new Dictionary<string, string>()
    let lineSps = File.ReadAllLines(path).Select(fun x -> (x.Split [| '=' |]))
    for x in lineSps do
      dict.Add(x.First(), String.Join("", x.Skip(1)))
    dict

module Connection =
    let private mkConnectionString (dataSource : string) =
        sprintf
            "Data Source = %s;"
            dataSource

    let mkShared () = new SqliteConnection (mkConnectionString (__SOURCE_DIRECTORY__ + "/prod.sqlite"))

module Types =
    [<CLIMutable>]
    type Mail =
        { Id         : int64
          Subject       : string
          FromAddr       : string
          }

module Queries =
    let private connectionF () = Connection.SqliteConnection (Connection.mkShared())

    let querySeqAsync<'R>          = querySeqAsync<'R> (connectionF)
    let querySingleAsync<'R>       = querySingleAsync<'R> (connectionF)
    let querySingleOptionAsync<'R> = querySingleOptionAsync<'R> (connectionF)

    module Mail =
      let insertMail subject fromAddr = querySingleOptionAsync<Types.Mail>{
        script "insert into mail(subject, from_addr) values(@Subject, @FromAddr);"
        parameters (dict [ "Subject", box subject; "FromAddr", box fromAddr ])
      }
      let selectMail = querySeqAsync<Types.Mail> {
        script "select * from mail;"
      }
