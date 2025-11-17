// Não vejo necessidade de no front termos a listagem de adiministradores, ja que não cabe ao usuário final ver uma lista do banco de dados.
import { useState } from "react";
import Adiministrador from "../interfaces/Adiministrador";
import { error } from "console";
import axios from "axios";
//Ja deixei os imports preparados, para não correr o risco de esquecermos, lembrem que devem instalar as bibliotecas localmente.

function CadastrarAdiministrador(){
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    function cadastrarAdiministrador(e : any){
        e.preventDefault()
        cadastrarAdiministradorAPI()
    }

    async function cadastrarAdiministradorAPI() {
        try{
                const adiministrador : Adiministrador = {
                    nome,
                    email,
                    senha,
                };
                const resposta = await axios.post("http://localhost:5162/api/administrador/cadastrar", adiministrador);
                console.log(resposta.data);
            }catch(error){
                console.log("Erro no cadastro do adiministrador: " + error);
            }
        }    
        return(
            <div>
            <h1>Cadastrar Adiministrador</h1>
            <form onSubmit={cadastrarAdiministrador}>
                <div>
                    <label>Nome:</label>
                    {/* No onChange você pode criar uma função separada pra cada um */}
                    <input onChange={(e : any) => setNome(e.target.value)} type="text" /> 
                </div>
                <div>
                    <label>Email:</label>
                    <input onChange={(e : any) => setEmail(e.target.value)} type="text" />
                </div>
                <div>
                    <label>Senha:</label>
                    <input onChange={(e : any) => setSenha(e.target.value)} type="text" />
                </div>
                <div>
                    <button type="submit">Cadastrar</button>   
                </div>
            </form>
        </div>
            
        )
}

export default CadastrarAdiministrador;