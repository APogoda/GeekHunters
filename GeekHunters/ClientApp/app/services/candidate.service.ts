import { Injectable } from '@angular/core';
import {Http} from "@angular/http";
import 'rxjs/add/operator/map';
import {CreateCandidate} from "../models/candidate";

@Injectable()
export class CandidateService {

  constructor(private http: Http) { }
  
  getCandidates(filter) {
    return this.http.get('/api/candidates'+'?'+this.toQueryString(filter))
        .map(res => res.json())
  }

    getCandidate(id) {
        return this.http.get('/api/candidates/'+id)
            .map(res => res.json());
    }
    
    updateCandidate(candidate:CreateCandidate){
      return this.http.put('/api/candidates/'+candidate.id,candidate)
          .map(res => res.json());
    }
  
  toQueryString(obj){
      let parts : any = [];
      for (let prop in obj){
          let value = obj[prop];
          if (value != null && value != undefined) 
              parts.push(encodeURIComponent(prop)+"="+encodeURIComponent(value))
      }
    return parts.join('&');
  }
  
  createCandidate(candidate) {
    return this.http.post('/api/candidates',candidate)
        .map(res => res.json())
  }

    deleteCandidate(id) {
        return this.http.delete('/api/candidates/'+id)
            .map(res => res.json());
    }
}
