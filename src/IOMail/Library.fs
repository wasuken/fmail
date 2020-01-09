namespace IOMail

open S22.Imap
open System.Collections.Generic
open System.Net.Mail
open System.Linq

module Imap =
  let getMessages (username: string) (password: string) (server: string) (port: int) : IEnumerable<MailMessage> =
    let client = new ImapClient(server, port, username, password, AuthMethod.Login, true)
    let uids : IEnumerable<uint32> = client.Search(SearchCondition.All())
    uids.Select(fun x -> client.GetMessage(x, FetchOptions.HeadersOnly))
