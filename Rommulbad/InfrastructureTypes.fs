namespace Rommulbad.Domain
open System

module Dto =

    type Candidate = {
        Name: string
        GuardianId: string
        Diploma: string
    }

    type Session = {
        Deep: bool
        Date: DateTime
        Minutes: int
    }

    type Guardian = {
        Id: string
        Name: string
        Candidates: List<Candidate>
}