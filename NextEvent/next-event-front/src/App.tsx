import React from "react";
import CadastrarAdiministrador from './componentes/CadastrarAdiministrador';
import CadastrarParticipante from './componentes/CadastrarParticipante'
import CadastrarEvento from './componentes/CadastrarEvento';
import AtualizarEvento from './componentes/AtualizarEvento';
import BuscarEventoID from './componentes/BuscarEventoID';
import ListarEventos from './componentes/ListarEventos';
import RealizarInscricao from "./componentes/RealizarInscricacao";
import {BrowserRouter, Route, Routes, Link} from "react-router-dom";
//Ja deixei os imports preparados, para não correr o risco de esquecermos, lembrem que devem instalar as bibliotecas localmente.

function App() {
  return (
   <div id="componente_app">
      <BrowserRouter>
        <nav>
          <ul>
            <li>
              <Link to="/eventos/listar">Lista de eventos</Link>
            </li>
            <li>
              <Link to="/eventos/inscricao">Inscrição em Eventos</Link>
            </li>
            <li>
              <Link to="/eventos/buscar">Pesquisar eventos</Link>
            </li>
            <li>
              <Link to="/eventos/atualizar">Atualizar Eventos</Link>
            </li>
             <li>
              <Link to="/cadastro/evento">Criar Evento</Link>
            </li>
            <li>
              <Link to="/cadastro/participante">Cadastro do Participante</Link>
            </li>
            <li>
              <Link to="/cadastro/adiministradores">Cadastro do Adiministrador</Link>
            </li>
          </ul>
        </nav>
         <Routes>
            <Route path="/eventos/listar" element={< ListarEventos/>} />
            <Route path="/eventos/inscricao" element={<RealizarInscricao/>} />
            <Route path="/eventos/buscar" element={<BuscarEventoID/>} />
            <Route path="/eventos/atualizar" element={<AtualizarEvento/>} />
            <Route path="/cadastro/evento" element={<CadastrarEvento/>} />
            <Route path="/cadastro/participante" element={<CadastrarParticipante/>} />
            <Route path="/cadastro/adiministradores" element={<CadastrarAdiministrador/>} />
          </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
