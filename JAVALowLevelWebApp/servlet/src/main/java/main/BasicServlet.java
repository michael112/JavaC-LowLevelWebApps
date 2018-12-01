package main;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.PrintWriter;

import javax.servlet.ServletException;
import java.io.IOException;

public class BasicServlet extends HttpServlet {

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        response.setStatus(200);
        response.setContentType("application/json;charset=UTF-8");
        String responseBody = "{" + "\n" + "\"imie\": \"Michał\"," + "\n" + "\"nazwisko\": \"Choromański\"," + "\n" + "\"wydzial\": \"Elektryczny\"," + "\n" + "\"kierunek\": \"Informatyka stosowana\"," + "\n" + "\"poziom\": \"magisterskie\"" + "\n" + "}";
        PrintWriter out = response.getWriter();
        out.print(responseBody);
    }

}
