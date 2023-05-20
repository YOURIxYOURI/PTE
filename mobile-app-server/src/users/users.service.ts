import { Injectable } from '@nestjs/common';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
import { PrismaService } from 'src/prisma.service';
import * as bcrypt from 'bcrypt';
@Injectable()
export class UsersService {
  constructor(private prisma: PrismaService) {}
  create(createUserDto: CreateUserDto) {
    return 'This action adds a new user';
  }

  findAll() {
    return `This action returns all users`;
  }

  async findOne(email: string) {
    const user = await this.prisma.users.findUnique({
      where: {
        email,
      },
    });
    if (user?.email) {
      return user;
    }
    return {
      answer: 'Użyj adresu email podannego w wniosku',
    };
  }

  async findOneLogin(updateUserDto: UpdateUserDto) {
    const user = await this.prisma.users.findUnique({
      where: {
        email: updateUserDto.email,
      },
    });
    if (user?.email && user?.password != '') {
      const isMatch = await bcrypt.compare(
        updateUserDto.password,
        user.password,
      );
      if (isMatch) {
        return {
          answer: 'Pomyślnie zalogowano',
        };
      } else {
        return {
          answer: 'Hasło nie poprawne',
        };
      }
    } else {
      return {
        answer: 'Konto Nie istnieje',
      };
    }
  }

  async update(updateUserDto: UpdateUserDto) {
    try {
      var re = /^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*\(\)\-\=\+_])(?=.{8,})/;
      if (!re.test(updateUserDto.password)) {
        return {
          answer:
            'Hasło powinno zawierać min. 8 znaków, znaki specjalne, duże i małe litery, oraz liczby',
        };
      }
      if (updateUserDto.password != updateUserDto.confpassword) {
        return { answer: 'sprawdź czy hasło są takie same' };
      }
      const salt = await bcrypt.genSalt(9);
      const hash = await bcrypt.hash(updateUserDto.password, salt);
      await this.prisma.users.update({
        where: {
          email: updateUserDto.email,
        },
        data: {
          password: hash,
        },
      });
      return { answer: 'Pomyślnie zarejestrowano' };
    } catch (error) {
      return { answer: 'Wystąpił błąd' };
    }
  }

  remove(id: number) {
    return `This action removes a #${id} user`;
  }
}
