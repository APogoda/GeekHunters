import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import {Http} from "@angular/http";
import {KeyValuePair} from "../models/candidate";

@Injectable()
export class SkillService {

  constructor(private http: Http) { }

    getSkills(filter) {
        return this.http.get('/api/skills'+'?'+this.toQueryString(filter))
            .map(res => res.json())
    }

    createSkill(skill:KeyValuePair) {
        return this.http.post('/api/skills',skill)
            .map(res => res.json())
    }

    deleteSkill(id) {
        return this.http.delete('/api/skills/'+id)
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
}
