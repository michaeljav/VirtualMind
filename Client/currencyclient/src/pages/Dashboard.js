import React, { Component } from 'react';
import axios from 'axios';
import Cookies from 'universal-cookie';
import 'bootstrap/dist/css/bootstrap.min.css'
import '../css/Login.css'

const baseUrl = "https://localhost:44303/api/Currency/change";
const baseUrlBank ="https://localhost:44303/api/Currency/";
const cookies = new Cookies();


class Dashboard extends Component {
  constructor(props){
    super(props);
    this.state = {
      form: {
        CbuyCurrencyToBuyType: '',
        CbuyCurrencyOrigenAmount: '',  
        cbuyCurrencyToBuyAmountCurrencyChanged:0,
        maxValueToExchange:0,
        dollarBuy:0,      
        dollarSell:0 ,     
        realBuy:0,      
        realSell:0,
        showMessage:false      
      }
   };
  }

  getCurrencyExchange = async (toSave) => {
    

        await axios
      .post(baseUrl,                
               {
                "CbuyCurrencyOrigenAmount": this.state.form.CbuyCurrencyOrigenAmount,
                "CbuyCurrencyToBuyType": this.state.form.CbuyCurrencyToBuyType,
                "toSave":toSave
               }           
                ,                
                {
                  headers: {
                    "Content-Type": 'application/json',
                    "Authorization": cookies.get('token')
                  },
                }
              )            
          .then(response => {
            
            return response.data;   
            
          })
          .then( response => {
            
             //if it couldn't do the currency exchange  launch a alert
             if(!response.success){
               alert(response.message)

               //Clear the input 
              this.setState({
                form:{
                   ...this.state.form,
                   ['CbuyCurrencyOrigenAmount'] : '',
                   ['cbuyCurrencyToBuyAmountCurrencyChanged'] : ''
                }
               });

               return;
             }
             
             this.setState({
              form:{
                 ...this.state.form,
                 ['cbuyCurrencyToBuyAmountCurrencyChanged'] : response.data.cbuyCurrencyToBuyAmountCurrencyChanged,               
                 ['maxValueToExchange'] : response.data.maxValueToExchange               
              }
             });
              
          })
          .catch(error => {
           if(this.state.form.CbuyCurrencyOrigenAmount > 0)
              alert('Favor Seleccionar moneda')
              console.log(error); 
              
              //Clear the input 
               this.setState({
                form:{
                   ...this.state.form,
                   ['CbuyCurrencyOrigenAmount'] : '',
                   ['cbuyCurrencyToBuyAmountCurrencyChanged'] : ''
                }
         });
          
            
          });
      /*END TEST DOING REQUEST*/
  }

/*MJM**/
  handleChanges = async (e) => {    
       
   
       await this.setState({
              form:{
                 ...this.state.form,
                 [e.target.name] : e.target.value
              }
       });
  
      
      //if it is dot doesn't calculate
       if(e.target.value !=="." 
          && e.target.value !=="Select" 
          && this.state.form.CbuyCurrencyOrigenAmount !=='' ){
        
       await this.getCurrencyExchange(false);
       }
       
     
  }

  logout=(e)=> {
    e.preventDefault();
    cookies.remove('token', {path: "/"});
    window.location.href="./";
  }

  SaveBuy = async(e,forSaving) =>  {
               e.preventDefault();              
              
     await axios
           .post(baseUrl,                
                 {
                  "CbuyCurrencyOrigenAmount": this.state.form.CbuyCurrencyOrigenAmount,
                  "CbuyCurrencyToBuyType": this.state.form.CbuyCurrencyToBuyType,
                  "toSave":forSaving
                 }             
                 ,                
                 {
                   headers: {
                     "Content-Type": "application/json",
                     "Authorization": cookies.get('token')
                   },
                 }
               )            
           .then(response => {
              return response.data;            
           })
           .then( response => {
           
            //if it couldn't do the currency exchange  launch a alert
            if(!response.success){
              alert(response.message)

              //Clear the input 
              this.setState({
                form:{
                   ...this.state.form,
                   ['CbuyCurrencyOrigenAmount'] : '',
                   ['cbuyCurrencyToBuyAmountCurrencyChanged'] : '',
                   ['showMessage'] : true
                   
                }
               });
               

              //  setTimeOut(() => this.setState({ show: true}), 3000)

              return;
            }

            //show message
             //Clear the input 
             this.setState({
              form:{
                 ...this.state.form,                 
                 ['showMessage'] : true                 
              }
             });
            //hidden the message
             setTimeout(
              function() {
                this.setState({
                  form:{
                     ...this.state.form,                 
                     ['showMessage'] : false                 
                  }
                 });
                 console.log('inside timeout')
              }
              .bind(this),
              4000);

            return response;
           })
           .catch(error => {
            console.log(error);
             return error; 
           });
   }

   
   getCurrentCurrency = async (currency) => {
     await axios
    .get(baseUrlBank+currency)            
    .then(response => {
       return response.data;  
    })
    .then( response => {
      
      let currencyTypeBuy,currencyTypeSell;
      if(currency === 'dolar'){
        currencyTypeBuy='dollarBuy'
        currencyTypeSell='dollarSell'
      }
      else if(currency === 'real'){
        currencyTypeBuy='realBuy'
        currencyTypeSell='realSell'      
      }
     
     
       this.setState({
        form:{
           ...this.state.form,
           [currencyTypeBuy] : response.data.buy,
           [currencyTypeSell]: response.data.sell          
        }
      });

     return response;
    })
    .catch(error => {
      return error; 
    });
  }


  async componentDidMount(){
   
  
    if(!cookies.get('token')){
       window.location.href="./";
      return;
    }
    await this.getCurrentCurrency('dolar');
    await this.getCurrentCurrency('real');

  }

  render() {

    
    return (      
      <div className="container" >       
        <div className="container-fluid"> 
        <div className="row">
          <div className="col-4">
            <p><strong>CAMBIO DE DIVISA</strong></p>
          </div>
          <div className=" col-6">
          <button id="btnlogout" className="btn btn-danger" onClick={(e) => this.logout(e)}>Log out</button> 
          </div>
        </div>  
        <hr/>   
        <hr/>   
         <div className="row">
           <div className="leftcontainer col-12">
              <div className="row"  >
                <div className="alig  col-12"  >
                  <label ><h6>Tasa Actual</h6></label>
                    <div className="row" /*style={{background: 'yellow'}}*/>
                       <div className="borderRight  col-6"  >
                         <label ><strong>Dolar</strong></label>
                       </div>
                       <div className="col-6"  >
                         <label ><strong>Real</strong></label>
                       </div>
                    </div>
                    <div className="row" >
                      <div className=" col-3"  >
              
                         Compra: <span><strong>{this.state.form.dollarBuy}</strong></span>
                      </div>
                      <div className="col-3 borderRight"  >
                         Venta: <span><strong>{this.state.form.dollarSell}</strong></span>
                      </div>
                      <div className="col-3"  >
                         Compra: <span><strong>{this.state.form.realBuy}</strong></span>
                       </div>
                       <div className="col-3"  >
                         Compra: <span><strong>{this.state.form.realSell}</strong></span>
                       </div>
                    </div>
                </div>
              </div>
              <hr/>
              <div className="row" >
                <div className="col-12">
                  <form >
                  <div className=" row"  >
                    <div className=" col-6">  
                    <div className="fle form-group">
                       <label  htmlFor="CbuyCurrencyToBuyType" >Tipo Moneda a Comprar:</label>
                       <select value={this.state.form.value} onChange={(e) => this.handleChanges(e)} name="CbuyCurrencyToBuyType">
                          <option>Select</option>
                           <option value="dolar">Dolar</option>
                           <option value="real">Real</option>
                       </select>
                       </div>
                    </div>                 
                    <div className="col-6" >
                      <div className="form-group">            
                        <label htmlFor="CbuyCurrencyOrigenAmount">Monto Peso Argentino: </label>            
                        <input type="text"  name="CbuyCurrencyOrigenAmount" className="form-control" onChange= {(e) => this.handleChanges(e)} value={this.state.form.CbuyCurrencyOrigenAmount} />
                      </div>
                    </div>
                  </div>
                  <div className=" rowTop row"  >
                    <div className="col-6"> 
                    <button className="btn btn-primary" onClick={(e) => this.SaveBuy(e,true)}>Comprar</button>  
                    </div>  
                    <div className="col-2" > 
                    <p>Total:</p>
                    </div>   
                    <div className="col-2" > 
                    <strong>{this.state.form.cbuyCurrencyToBuyAmountCurrencyChanged}</strong>
                    </div>                
                  </div> 
                  <div className="row" >
                    {this.state.form.showMessage ? <p ><strong>Compra Exitosa!</strong></p>: null}
                    </div>  
                           
                  </form>
                </div>              
              </div>
         </div>
         </div>
         </div>
      </div>   

               
        
      
      
        
        
     
    );
  }
}

export default Dashboard;
