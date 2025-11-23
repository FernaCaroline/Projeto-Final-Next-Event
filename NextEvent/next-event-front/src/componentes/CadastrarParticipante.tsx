import { useState } from "react";
import Participante from "../interfaces/Participante";
import axios from "axios";

//Anna

function CadastrarParticipante() {
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    function cadastrarParticipante(e: any) {
        e.preventDefault();
        cadastrarParticipanteAPI();
    }

    async function cadastrarParticipanteAPI() {
        try {
            const participante: Participante = {
                nome,
                email,
                senha
            };

            const resposta = await axios.post(
                "http://localhost:5162/api/participante/cadastrar",
                participante
            );

            console.log(resposta.data);
        } catch (error) {
            console.log("Erro no cadastro do participante: " + error);
        }
    }

    return (
        <div>
            <h1>Cadastrar Participante</h1>
            <form onSubmit={cadastrarParticipante}>
                <div>
                    <label>Nome:</label>
                    <input type="text" onChange={(e) => setNome(e.target.value)} />
                </div>
                <div>
                    <label>Email:</label>
                    <input type="text" onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div>
                    <label>Senha:</label>
                    <input type="password" onChange={(e) => setSenha(e.target.value)} />
                </div>
                <div>
                    <button type="submit">Cadastrar</button>
                </div>
            </form>
        </div>
    );
}

export default CadastrarParticipante;
