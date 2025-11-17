import { useState } from "react";
import Inscricao from "../interfaces/Inscricao";
import { error } from "console";
import axios from "axios";
//Ja deixei os imports preparados, para não correr o risco de esquecermos, lembrem que devem instalar as bibliotecas localmente.

//Anna - tentar fazer, talvez tenha que tirar duvidas na próxima aula

function RealizarInscricao(){
    
    const [participanteId, setParticipanteId] = useState("");
    const [eventoId, setEventoId] = useState("");

    function realizarInscricao(e : any){
        e.preventDefault()
        realizarInscricaoAPI()
    }

    async function realizarInscricaoAPI() {
        try{
            const inscricao : Inscricao = {
                participanteId,
                participante: "",
                eventoId,
                evento: "",
            };
            // Me obrigou a colocar participante e evento, pq na definição da interface Inscricao eles estão como obrigatórios.
            const resposta = await axios.post("http://localhost:5162/api/inscricao/cadastrar", inscricao)
            console.log(resposta.data);
        }catch(error){
            console.log("Erro ao realizar inscrição: " + error);
        }
    }

        return(
         <div>
            <h1>Realizar Inscrição</h1>
            <form onSubmit={realizarInscricao}>
                <div>
                    <label>Id do participante:</label>
                    <input onChange={(e : any) => setParticipanteId(e.target.value)} type="text" /> 
                </div>
                <div>
                    <label>Id do evento:</label>
                    <input onChange={(e : any) => setEventoId(e.target.value)} type="text" /> 
                </div>
                <div>
                    <button type="submit">Inscrever-se</button>   
                </div>
             </form>
        </div>
        )
    }

export default RealizarInscricao;