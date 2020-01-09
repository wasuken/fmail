namespace IOConfig

open System
open System.IO
open System.Linq
open System.Collections.Generic

module Ini =
  let readIniToDict (path:string) =
    let mutable dict = new Dictionary<string, string>()
    let lineSps = File.ReadAllLines(path).Select(fun x -> (x.Split [|'='|]))
    for x in lineSps do
      dict.Add(x.First(), String.Join("", x.Skip(1)))
    dict
