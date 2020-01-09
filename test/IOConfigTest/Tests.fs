module Tests

open System
open Xunit
open IOConfig.Ini
open System.Linq

[<Fact>]
let ``Read ini base test`` () =
  let iniMap = readIniToDict "config.ini"
  let expectedTps = [("UNAME", "hoge"); ("PASS", "fuga"); ("SNAME", "foo"); ("PORT", "1024")]
  Assert.Equal(4, iniMap.Count)
  for tp in expectedTps do
    let (k, v) = tp
    Assert.Equal(v, iniMap.[k])
