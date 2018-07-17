export interface KeyValuePair {
    id:number,
    name:string
}

export interface Candidate {
    id:number, 
    firstName:string,
    lastName:string,
    skills:KeyValuePair[],
    skillsString:string
}

export interface CreateCandidate {
    id:number,
    firstName:string,
    lastName:string,
    skills:number[]
}