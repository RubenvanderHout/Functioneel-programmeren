namespace Rommulbad.Domain

open Thoth.Json.Net
open System
open System.Text.RegularExpressions

type Name = private Name of string
type NameErrors =
    | InvalidSpaceAtStart
    | InvalidSpaceAtEnd
    | InvalidCharacters

module Name =
    let make (name: string) : Result<string, NameErrors>=
        let stringWithoutSpaces = name.Split([|' '|])

        let ContainsOnlyLetters = stringWithoutSpaces |> Seq.forall (fun s -> s |> Seq.forall System.Char.IsLetter)
        let StartWithSpace = name.EndsWith(" ")
        let EndsWithSpace = name.StartsWith(" ")

        match (StartWithSpace, EndsWithSpace, ContainsOnlyLetters) with
        | (false, false, true) -> Ok(name)
        | (true, _, _) -> Error InvalidSpaceAtStart
        | (_, true, _) -> Error InvalidSpaceAtEnd
        | (_, _, false) -> Error InvalidCharacters


type CandidateName = CandidateName of Name
type GuardianName = GuardianName of Name

type MinutesErrors =
    | NegativeMinutesError
    | SmallerThan30Error

module Minutes =
    let isPostiveInt (minutes : int) =
        if minutes > 0 then
            Ok(minutes)
        else
            Error(NegativeMinutesError)
    let isSmallerThan30 (minutes : int) =
        if minutes <= 30 then
            Ok(minutes)
        else
            Error(SmallerThan30Error)
    let make (minutes : int) : Result<int, MinutesErrors > =
        isPostiveInt(minutes)
        |> Result.bind isSmallerThan30

type Minutes = private Minutes of int

type Diploma =
 | A
 | B
 | C
 | None

type Deep = Depth of String
type Shallow = Shallow of string
type PoolDepth =
 | Depth
 | Shallow

type GuardianIdErrors =
    | InvalidLength
    | InvalidPrefix
    | InvalidDash
    | InvalidSuffix


module GuardianId =
    let make (id : String) : Result<string, GuardianIdErrors> =
        if id.Length <> 8 then
            Error InvalidLength
        else
            let prefix = id.Substring(0, 3)
            let dash = id[3]
            let suffix = id.Substring(4, 4)

            let isPrefixValid = prefix |> Seq.forall System.Char.IsDigit
            let isDashValid = dash = '-'
            let isSuffixValid = suffix |> Seq.forall (fun c -> System.Char.IsUpper(c) && System.Char.IsLetter(c))

            match (isPrefixValid, isDashValid, isSuffixValid) with
            | (true, true, true) -> Ok(id)
            | (false, _, _) -> Error InvalidPrefix
            | (_, false, _) -> Error InvalidDash
            | (_, _, false) -> Error InvalidSuffix


type GuardianId = private GuardianId of string


module Domain =
    type Candidate ={
        Name: CandidateName
        GuardianId: GuardianId
        Diploma: Diploma
    }

    type Session = {
        Deep: PoolDepth
        Date: DateTime
        Minutes: Minutes
    }

    type Guardian = {
        Id: string
        Name: GuardianName
        Candidates: List<Option<Candidate>>
    }