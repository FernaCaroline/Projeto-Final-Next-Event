import { useState } from "react";
import Evento from "../interfaces/Evento";
import { error } from "console";
import axios from "axios";
//Ja deixei os imports preparados, para não correr o risco de esquecermos, lembrem que devem instalar as bibliotecas localmente.

function CadastrarEvento(){
    const [nome, setNome] = useState("");
    const [descricao, setDescricao] = useState("");
    const [dataInicio, setDataInicio] = useState("");
    const [dataFim, setDataFim] = useState("");

    // descricao: string;
    // // . Precisa ver qual o tipo de dado que tem que colocar nas datas, eu não lembro, no DB é DateTime
    // dataInicio: string; 
    // dataFim

    function cadastrarEvento(e : any){
        e.preventDefault()
        cadastrarEventoAPI()
    }

    async function cadastrarEventoAPI() {
        try{
                const evento : Evento = {
                    nome,
                    descricao,
                    dataInicio,
                    dataFim,
                };
                const resposta = await axios.post("http://localhost:5162/api/evento/cadastrar", evento);
                console.log(resposta.data);
            }catch(error){
                console.log("Erro no cadastro do evento: " + error);
            }
        }    
        return(
            <div>
            <h1>Cadastrar Evento</h1>
            <form onSubmit={cadastrarEvento}>
                <div>
                    <label>Nome:</label>
                    {/* No onChange você pode criar uma função separada pra cada um */}
                    <input onChange={(e : any) => setNome(e.target.value)} type="text" /> 
                </div>
                <div>
                    <label>Email:</label>
                    <input onChange={(e : any) => setDescricao(e.target.value)} type="text" />
                </div>
                <div>
                    <label>Data Início:</label>
                    <input onChange={(e : any) => setDataInicio(e.target.value)} type="datetime-local" />
                </div>
                <div>
                    <label>Data Fim:</label>
                    <input onChange={(e : any) => setDataFim(e.target.value)} type="datetime-local" />
                </div>
                <div>
                    <button type="submit">Cadastrar</button>   
                </div>
            </form>
        </div>
            
        )
}

export default CadastrarEvento;