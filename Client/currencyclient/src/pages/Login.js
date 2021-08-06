import React, { Component } from 'react';

import '../css/Login.css'
import 'bootstrap/dist/css/bootstrap.min.css'
import axios from 'axios';
import md5 from 'md5';
import Cookies from 'universal-cookie';

const baseUrl="https://localhost:44303/User/authenticate"
const cookies = new Cookies();

class Login extends Component {
 
  constructor(props){
    super(props);
    this.state = {
      form: {
        username: '',
        password: ''
      }
   };
  }

  
  handleChange =  async e => { 
    // let t ={...this.state.form}
    // console.log('{...this.state.form}============== '+JSON.stringify(t))  
    // console.log(`[e.target.name] ========== ${[e.target.name]} y e.target.value ========== ${e.target.value}`)  
   
                await  this.setState({
                                       form: {
                                                ...this.state.form,
                                               [e.target.name]: e.target.value
                                               }                                      
                                     });   
                               }

  login =async(e) =>  {
   e.preventDefault();
   
    await axios
          .post(baseUrl,
                {
                "username": this.state.form.username,
                "password": md5(this.state.form.password)
                } ,                
                {
                  headers: {
                    "Content-Type": "application/json",
                    
                  },
                }
              )            
          .then(response => {
            return response.data;
          })
          .then( response => {
           
            if(response.success){
                cookies.set('token', response.data.token, {path: "/"});
              window.location.href = "./dashboard";
                
            }else {
              alert(response.message)
            }
          })
          .catch(error => {
            console.log(error); 
          });
  }
  
  componentDidMount(){
    if(cookies.get('token')){

      window.location.href="./dashboard";
      
    }
  }
  render() {
    return (
      <div className="mainContainer">
        <div className="secondaryContainer">
          <div className="form-group">
            <label htmlFor="username">User Name: </label>
            <br/>
            <input type="text" name="username" className="form-control" onChange= {this.handleChange} />
            <br/>
            <label htmlFor="password">Password: </label>
            <br/>
            <input type="password" name="password" className="form-control"  onChange= {this.handleChange} />          
            <br/>
            <button className="btn btn-primary" onClick={(e) => this.login(e)}>Log in </button>
            <br/>
            <hr/>           
          </div>
        </div>
      </div>
    );
  }
}

export default Login;
