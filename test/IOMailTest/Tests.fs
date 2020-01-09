module Tests

open System
open Xunit
open IOMail.Imap
open IOConfig.Ini
open System.Linq

[<Fact>]
let ``Get Imap Test`` () =
  let iniMap = readIniToDict "config.ini"
  let msgs = getMessages iniMap.["UNAME"] iniMap.["PASS"] iniMap.["SNAME"]  (iniMap.["PORT"] |> int)
  Assert.True(msgs.Count() > 0)
