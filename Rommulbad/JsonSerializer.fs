namespace Rommulbad

open Thoth.Json.Net

module JsonSerializer =

    let serialize<'T> (value: 'T) : string =
        Encode.Auto.toString(4, value)

    let deserialize<'T> (json: string) : Result<'T, string> =
        Decode.Auto.fromString<'T>(json)