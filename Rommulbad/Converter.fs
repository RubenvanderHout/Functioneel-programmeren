namespace Rommulbad

open Thoth.Json.Net
open System
open Rommulbad.Domain



module Converter =

    let dtoToDomain<'Dto, 'Domain> (convert: 'Dto -> 'Domain) (dto: 'Dto) : 'Domain =
        convert dto
    let domainToDto<'Domain, 'Dto> (convert: 'Domain -> 'Dto) (domain: 'Domain) : 'Dto =
        convert domain






module Candidate =
    let encode: Encoder<Domain.Candidate> =
        fun candidate ->
            Encode.object
                [ "name", Encode.string candidate.Name
                  "guardian_id", Encode.string candidate.GuardianId
                  "diploma", Encode.string candidate.Diploma ]

    let decode: Decoder<Dto> =
        Decode.object (fun get ->
            { Name = get.Required.Field "name" Decode.string
              GuardianId = get.Required.Field "guardian_id" Decode.string
              Diploma = get.Required.Field "diploma" Decode.string })



// module Session =
//     let encode: Encoder<Domain.Session> =
//         fun session ->

//             Encode.object
//                 [ "deep", Encode.bool session.Deep
//                   "date", Encode.datetime session.Date
//                   "amount", Encode.int session.Minutes ]

//     let decode: Decoder<Dto.Session> =
//         Decode.object (fun get ->
//             { Deep = get.Required.Field "deep" Decode.bool
//               Date = get.Required.Field "date" Decode.datetime
//               Minutes = get.Required.Field "amount" Decode.int })
